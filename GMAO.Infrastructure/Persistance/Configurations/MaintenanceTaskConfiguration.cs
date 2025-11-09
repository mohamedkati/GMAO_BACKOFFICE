using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class MaintenanceTaskConfiguration : IEntityTypeConfiguration<MaintenanceTask>
    {
        public void Configure(EntityTypeBuilder<MaintenanceTask> builder)
        {
            builder.ToTable("maintenance_tasks");
            builder.HasKey(mt => mt.Id);

            builder.Property(mt => mt.TaskOrder).IsRequired();
            builder.Property(mt => mt.Description).IsRequired().HasMaxLength(1000);
            builder.Property(mt => mt.EstimatedDurationMinutes);

            builder.HasOne(mt => mt.MaintenancePlan).WithMany(mp => mp.Tasks)
                .HasForeignKey(mt => mt.MaintenancePlanId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(mt => mt.RequiredSkill).WithMany()
                .HasForeignKey(mt => mt.RequiredSkillId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(mt => mt.MaintenancePlanId);
        }
    }
}
