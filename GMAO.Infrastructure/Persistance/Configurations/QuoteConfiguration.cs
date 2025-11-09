using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.ToTable("quotes");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Reference).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Title).IsRequired().HasMaxLength(200);
            builder.Property(q => q.Description).HasMaxLength(2000);
            builder.Property(q => q.Terms).HasMaxLength(2000);
            builder.Property(q => q.SubTotal).IsRequired().HasPrecision(18, 2);
            builder.Property(q => q.DiscountPercent).HasPrecision(5, 2);
            builder.Property(q => q.DiscountAmount).HasPrecision(18, 2);
            builder.Property(q => q.VATAmount).HasPrecision(18, 2);
            builder.Property(q => q.TotalAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(q => q.ValidUntil).IsRequired();
            builder.Property(q => q.Status).IsRequired().HasConversion<int>();
            builder.Property(q => q.SentAt);
            builder.Property(q => q.ViewedAt);
            //builder.Property(q => q.AcceptanceType).HasConversion<int>();
            builder.Property(q => q.AcceptedAt);
            builder.Property(q => q.Version).IsRequired();
            builder.Property(q => q.PreviousVersionId);

            builder.Ignore(q => q.IsExpired);
            //builder.Ignore(q => q.AcceptedLineIds);

            builder.HasOne(q => q.QuoteRequest).WithOne(qr => qr.Quote)
                .HasForeignKey<Quote>(q => q.QuoteRequestId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(q => q.Customer).WithMany()
                .HasForeignKey(q => q.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(q => q.Lines).WithOne(l => l.Quote)
                .HasForeignKey(l => l.QuoteId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.WorkOrders).WithOne(wo => wo.Quote)
                .HasForeignKey(wo => wo.QuoteId);

            builder.HasIndex(q => q.Reference).IsUnique();
            builder.HasIndex(q => q.CustomerId);
            builder.HasIndex(q => q.Status);
            builder.HasIndex(q => q.ValidUntil);
            builder.HasIndex(q => q.TenantId);
            builder.Ignore(q => q.DomainEvents);
        }
    }
    public class ServiceContractConfiguration : IEntityTypeConfiguration<ServiceContract>
    {
        public void Configure(EntityTypeBuilder<ServiceContract> builder)
        {
            builder.ToTable("service_contracts");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Reference).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Type).IsRequired().HasConversion<int>();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).HasMaxLength(2000);
            builder.Property(c => c.StartDate).IsRequired();
            builder.Property(c => c.EndDate).IsRequired();
            builder.Property(c => c.Status).IsRequired().HasConversion<int>();
            builder.Property(c => c.AutoRenewal).IsRequired();
            builder.Property(c => c.PricingModel).IsRequired().HasConversion<int>();
            builder.Property(c => c.MonthlyAmount).HasPrecision(18, 2);
            builder.Property(c => c.AnnualAmount).HasPrecision(18, 2);

            builder.HasOne(c => c.Customer).WithMany(cu => cu.Contracts)
                .HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Pricing).WithOne(p => p.ServiceContract)
                .HasForeignKey<ContractPricing>(p => p.ServiceContractId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Consumptions).WithOne(co => co.ServiceContract)
                .HasForeignKey(co => co.ServiceContractId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.Reference).IsUnique();
            builder.HasIndex(c => c.CustomerId);
            builder.HasIndex(c => c.Status);
            builder.HasIndex(c => c.StartDate);
            builder.HasIndex(c => c.EndDate);
            builder.HasIndex(c => c.TenantId);
            builder.Ignore(c => c.DomainEvents);
        }
    }

}
