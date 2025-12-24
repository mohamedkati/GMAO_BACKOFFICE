using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("sites");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Reference).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Type).IsRequired().HasConversion<int>();
            builder.Property(s => s.BuildingYear);
            builder.Property(s => s.TotalArea).HasPrecision(10, 2);
            builder.Property(s => s.FloorsCount);
            builder.Property(s => s.UnitsCount);
            builder.Property(x => x.Comment).IsRequired(false).HasMaxLength(4000);
            builder.Property(x => x.CommentReport).IsRequired(false).HasMaxLength(4000);
            builder.Property(x => x.Siret).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.InvoiceMailAddress).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.MainMailAddress).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Siren).IsRequired(false).HasMaxLength(50);

            // Value Object: Address
            builder.OwnsOne(s => s.Address, a =>
            {
                a.Property(x => x.Street).HasMaxLength(200).IsRequired();
                a.Property(x => x.SecondAddressLine).HasMaxLength(200);
                a.Property(x => x.City).HasMaxLength(100).IsRequired();
                a.Property(x => x.FirstAddressLine).HasMaxLength(100);
                a.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
                a.Property(x => x.Country).HasMaxLength(100).IsRequired();
            });

            builder.OwnsOne(s => s.BillingAddress, a =>
            {
                a.Property(x => x.Street).HasColumnName("BillingStreet").HasMaxLength(200).IsRequired();
                a.Property(x => x.SecondAddressLine).HasColumnName("BillingLigne2").HasMaxLength(200);
                a.Property(x => x.City).HasColumnName("BillingCity").HasMaxLength(100).IsRequired();
                a.Property(x => x.FirstAddressLine).HasColumnName("BillingLigne1").HasMaxLength(100);
                a.Property(x => x.PostalCode).HasColumnName("BillingPostalCode").HasMaxLength(20).IsRequired();
                a.Property(x => x.Country).HasColumnName("BillingCountry").HasMaxLength(100).IsRequired();
            });

            // Value Object: GeoCoordinates
            builder.OwnsOne(s => s.Coordinates, c =>
            {
                c.Property(x => x.Latitude).HasPrecision(10, 7);
                c.Property(x => x.Longitude).HasPrecision(10, 7);
            });

            builder.HasOne(s => s.Customer).WithMany(c => c.Sites)
                .HasForeignKey(s => s.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Units).WithOne(u => u.Site)
                .HasForeignKey(u => u.SiteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Assets).WithOne(a => a.Site)
                .HasForeignKey(a => a.SiteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ClientType)
                .WithMany(x => x.Sites).HasForeignKey(x => x.ClientTypeId).IsRequired(true);

            builder.HasOne(x => x.Commercial)
                  .WithMany(x => x.SiteCommercials)
                  .HasForeignKey(x => x.CommercialId);

            builder.HasOne(x => x.OperationsManager)
             .WithMany(x => x.OperationManagerForSites)
             .HasForeignKey(x => x.OperationsManagerId);

            builder.HasOne(x => x.Technician1)
                .WithMany(x => x.Technician1ForSites)
                .HasForeignKey(x => x.Technician1Id);

            builder.HasOne(x => x.Technician2)
                .WithMany(x => x.Technician2ForSites)
                .HasForeignKey(x => x.Technician2Id);

            builder.HasOne(x => x.SectorManager)
               .WithMany(x => x.SiteSectorManagers)
               .HasForeignKey(x => x.SectorManagerId);

            builder.HasOne(x => x.ClientContact)
                .WithMany(x => x.Sites)
                .HasForeignKey(x => x.ClientContactId).IsRequired(false);

            builder.HasOne(p => p.PaymentMethod)
                .WithMany(x => x.Sites)
                .HasForeignKey(x => x.PaymentMethodId).IsRequired(false);

            builder.HasOne(x => x.VAT)
                .WithMany(x => x.Sites)
                .HasForeignKey(x => x.VatId)
                .IsRequired(true);

            builder.OwnsOne(x => x.SiteAccessInfo, c =>
            {
                c.Property(x => x.AccessCodes).HasMaxLength(255).IsRequired(false);
                c.Property(x => x.SafetyRequirements).HasMaxLength(255).IsRequired(false);
                c.Property(x => x.ParkingInfo).HasMaxLength(255).IsRequired(false);
                c.Property(x => x.RequiresBadge).IsRequired(false);
                c.Property(x => x.AccessRestrictions).HasMaxLength(255).IsRequired(false);
                c.Property(x => x.GeneralInstructions).HasMaxLength(255).IsRequired(false);
                c.Property(x => x.KeyInstructions).HasMaxLength(255).IsRequired(false);
                c.Property(x => x.WorkingHours).HasMaxLength(255).IsRequired(false);
            });

            builder.HasIndex(s => s.Reference).IsUnique();
            builder.HasIndex(s => s.CustomerId);
            builder.HasIndex(s => s.CommercialId);
            builder.HasIndex(s => s.Technician1Id);
            builder.HasIndex(s => s.OperationsManagerId);
            builder.HasIndex(s => s.ClientTypeId);
            builder.HasIndex(s => s.SectorManagerId);
            builder.HasIndex(s => s.Type);

            //builder.HasIndex(x => x.Coordinates);
            builder.HasIndex(x => x.SectorTypeId);
            builder.HasIndex(s => s.Type);
            builder.HasIndex(s => s.TenantId);
            builder.Ignore(s => s.DomainEvents);


        }
    }
}
