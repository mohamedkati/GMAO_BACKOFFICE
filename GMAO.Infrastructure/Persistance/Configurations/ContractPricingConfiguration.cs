using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class ContractPricingConfiguration : IEntityTypeConfiguration<ContractPricing>
    {
        public void Configure(EntityTypeBuilder<ContractPricing> builder)
        {
            builder.ToTable("contract_pricings");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Model).IsRequired().HasConversion<int>();
            builder.Property(p => p.FixedMonthlyFee).HasPrecision(18, 2);
            builder.Property(p => p.IncludedHours);
            builder.Property(p => p.HourlyRateAfterIncluded).HasPrecision(10, 2);
            builder.Property(p => p.EmergencyCalloutFee).HasPrecision(10, 2);
            builder.Property(p => p.PartsMarkupPercent).HasPrecision(5, 2);

            builder.HasOne(p => p.ServiceContract).WithOne(c => c.Pricing)
                .HasForeignKey<ContractPricing>(p => p.ServiceContractId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.ServiceContractId).IsUnique();
        }
    }

}
