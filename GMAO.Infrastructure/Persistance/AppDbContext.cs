using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Common.Interfaces.Infrastructure;
using GMAO.Application.Common.Interfaces.Services;
using GMAO.Domain.Common;
using GMAO.Domain.Entities;
using GMAO.Domain.Entities.Auth;
using GMAO.Domain.Interfaces;
using GMAO.Infrastructure.Persistance.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IAppDbContext, IUnitOfWork
    {
        private readonly IDomainEventDispatcher _mediatorDispatcher;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IDatetimeService _datetimeService;

        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher mediatorDispatcher, IAuthenticatedUser authenticatedUser, IDatetimeService datetimeService) : base(options)
        {
            this._mediatorDispatcher = mediatorDispatcher;
            this._authenticatedUser = authenticatedUser;
            this._datetimeService = datetimeService;
        }
        public DbSet<T> SetEntity<T>() where T : BaseEntity<Guid> => Set<T>();

        // Domaine
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Site> Sites => Set<Site>();
        public DbSet<AssetType> AssetTypes => Set<AssetType>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
        public DbSet<WorkOrderLog> WorkOrderLogs => Set<WorkOrderLog>();
        public DbSet<Quote> Quotes => Set<Quote>();
        public DbSet<QuoteLine> QuoteLines => Set<QuoteLine>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        //public DbSet<Part> Parts => Set<Part>();
        //public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        //public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();
        //public DbSet<Invoice> Invoices => Set<Invoice>();
        //public DbSet<Contract> Contracts => Set<Contract>();

        // Auth métier
        public DbSet<Role> DomainRoles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<TenantUser> TenantUsers => Set<TenantUser>();
        public DbSet<Staff> Staffs => Set<Staff>();

        private IDbContextTransaction? _currentTransaction;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //
            //  Charger automatiquement toutes les configurations du dossier Configurations/
            //
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            //
            //  Tables Identity (schéma auth)
            //
            builder.Entity<ApplicationUser>().ToTable("Users", "auth");
            builder.Entity<ApplicationRole>().ToTable("Roles", "auth");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "auth");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "auth");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "auth");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "auth");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "auth");

            //
            // Convention globale pour les nombres décimaux
            //
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
            //
            //  Convention globale pour les dates (UTC)
            //
            builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
                .ToList()
                .ForEach(p => p.SetColumnType("datetime2"));

            // set string default value ""
            builder.Model.GetEntityTypes()
             .SelectMany(t => t.GetProperties())
             .Where(p => p.ClrType == typeof(string))
             .ToList()
             .ForEach(p => p.SetDefaultValue(""));

            builder.Model.GetEntityTypes()
                .Where(entity => typeof(BaseEntity<>).IsAssignableFrom(entity.ClrType))
              .SelectMany(t => t.GetProperties())
              .Where(p => p.ClrType == typeof(Guid) && p.IsNullable == false && p.Name == "Id")
              .ToList()
              .ForEach(p => p.IsPrimaryKey());



            //
            //  Lien entre TenantUser.UserId → auth.Users(Id)
            //
            //builder.Entity<TenantUser>()
            //    .HasOne<ApplicationUser>()
            //    .WithMany()
            //    .HasForeignKey(tu => tu.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            

           // app user id must be the same as staff id.
            builder.Entity<Staff>()
               .HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(tu => tu.Id)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

            // TODO APPLY query filter on tenantId and inject currentTenantId
            // TODO apply global query filter for soft delete
            builder.Model.GetEntityTypes()
                .Where(entity => typeof(BaseEntity<>).IsAssignableFrom(entity.ClrType))
                .ToList()
                .ForEach(x => x.SetQueryFilter(BuildTenantAndNotDeletedFilter(x.ClrType)));


        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
                         .Where(e => e.Entity is Domain.Common.BaseAuditableEntity &&
                                     (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));
            foreach (var entry in entities)
            {
                var auditable = (Domain.Common.BaseAuditableEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = _datetimeService.UtcNow;
                    if (auditable.Id == Guid.Empty)
                        auditable.Id = Guid.NewGuid();
                    auditable.CreatedBy = _authenticatedUser.IsAuthenticated() ? _authenticatedUser.UserId : Guid.Empty; // TODO: Récupérer l'ID de l'utilisateur courant
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditable.LastModifiedBy = _authenticatedUser.IsAuthenticated() ? _authenticatedUser.UserId : Guid.Empty; // TODO: Récupérer l'ID de l'utilisateur courant
                    auditable.LastModifiedAt = _datetimeService.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Soft Delete
                    entry.State = EntityState.Modified;
                    auditable.IsDeleted = true;
                    auditable.DeletedOn = _datetimeService.UtcNow;
                    auditable.DeletedBy = _authenticatedUser.IsAuthenticated() ? _authenticatedUser.UserId : Guid.Empty; // TODO: Récupérer l'ID de l'utilisateur courant
                }

            }

            // Collect all domain events, publish them and mark them as published
            var domainEvents = ChangeTracker.Entries<BaseEntity<Guid>>()
                .Where(e => e.Entity.DomainEvents.Any())
                .SelectMany(e => e.Entity.DomainEvents)
                .Where(e => !e.IsPublished)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            // Publish Domain Events after saving changes 
            if (domainEvents.Any())
            {
                await _mediatorDispatcher.DispatchDomainEventsAsync(domainEvents, cancellationToken);

                foreach (var domainEvent in domainEvents)
                    domainEvent.IsPublished = true;
            }
            return result;
        }

        public async Task StartTransactionAsync()
        {
            if (_currentTransaction is not null)
                return;

            _currentTransaction = await this.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction is null)
                return;

            await _currentTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null)
                return;

            await _currentTransaction.RollbackAsync();
        }

        public async Task<int> SaveAllAsync(CancellationToken cancellationToken = default!)
        {
            return await SaveChangesAsync(cancellationToken);
        }



        #region Helpers
        private LambdaExpression BuildTenantAndNotDeletedFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var isDeleted = Expression.Property(parameter, nameof(BaseAuditableEntity.IsDeleted));
            var tenantId = Expression.Property(parameter, nameof(BaseAuditableEntity.TenantId));

            var deletedFilter = Expression.Equal(isDeleted, Expression.Constant(false));
            var tenantFilter = Expression.Equal(tenantId, Expression.Constant(_authenticatedUser.TenantId));

            var final = Expression.AndAlso(deletedFilter, tenantFilter);
            return Expression.Lambda(final, parameter);
        }
        #endregion
    }
}
