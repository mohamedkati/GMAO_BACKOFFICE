using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class MaintenancePlanConfiguration : IEntityTypeConfiguration<MaintenancePlan>
    {
        public void Configure(EntityTypeBuilder<MaintenancePlan> builder)
        {
            builder.ToTable("maintenance_plans");
            builder.HasKey(mp => mp.Id);

            builder.Property(mp => mp.Name).IsRequired().HasMaxLength(200);
            builder.Property(mp => mp.Frequency).IsRequired().HasConversion<int>();
            builder.Property(mp => mp.LastExecutionDate);
            builder.Property(mp => mp.NextExecutionDate);
            builder.Property(mp => mp.IsActive).IsRequired();
            builder.Property(mp => mp.AlertDaysBefore).IsRequired();

            builder.HasOne(mp => mp.Asset).WithMany(a => a.MaintenancePlans)
                .HasForeignKey(mp => mp.AssetId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(mp => mp.Tasks).WithOne(mt => mt.MaintenancePlan)
                .HasForeignKey(mt => mt.MaintenancePlanId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(mp => mp.AssetId);
            builder.HasIndex(mp => mp.NextExecutionDate);
            builder.HasIndex(mp => mp.TenantId);
            builder.Ignore(mp => mp.DomainEvents);
        }
    }
}
