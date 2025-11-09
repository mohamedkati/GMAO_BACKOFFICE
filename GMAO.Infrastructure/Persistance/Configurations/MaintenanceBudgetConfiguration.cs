using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class MaintenanceBudgetConfiguration : IEntityTypeConfiguration<MaintenanceBudget>
    {
        public void Configure(EntityTypeBuilder<MaintenanceBudget> builder)
        {
            builder.ToTable("maintenance_budgets");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Year).IsRequired();
            builder.Property(b => b.BudgetedAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(b => b.CommittedAmount).HasPrecision(18, 2);
            builder.Property(b => b.InvoicedAmount).HasPrecision(18, 2);
            builder.Property(b => b.AlertThreshold).HasPrecision(5, 2);
            builder.Property(b => b.AlertSent).IsRequired();

            builder.Ignore(b => b.RemainingBudget);
            builder.Ignore(b => b.ConsumptionPercent);

            builder.HasOne(b => b.Customer).WithMany(c => c.MaintenanceBudgets)
                .HasForeignKey(b => b.CustomerId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(b => b.CustomerId);
            builder.HasIndex(b => b.Year);
            builder.HasIndex(b => b.TenantId);
            builder.Ignore(b => b.DomainEvents);
        }
    }


}
