using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class ContractConsumptionConfiguration : IEntityTypeConfiguration<ContractConsumption>
    {
        public void Configure(EntityTypeBuilder<ContractConsumption> builder)
        {
            builder.ToTable("contract_consumptions");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Month).IsRequired();
            builder.Property(c => c.Year).IsRequired();
            builder.Property(c => c.HoursConsumed).IsRequired();
            builder.Property(c => c.AmountCharged).IsRequired().HasPrecision(18, 2);
            builder.Property(c => c.WorkOrdersCount).IsRequired();

            builder.HasOne(c => c.ServiceContract).WithMany(sc => sc.Consumptions)
                .HasForeignKey(c => c.ServiceContractId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.ServiceContractId);
            builder.HasIndex(c => new { c.ServiceContractId, c.Year, c.Month }).IsUnique();
            builder.HasIndex(c => c.TenantId);
            builder.Ignore(c => c.DomainEvents);
        }
    }

}
