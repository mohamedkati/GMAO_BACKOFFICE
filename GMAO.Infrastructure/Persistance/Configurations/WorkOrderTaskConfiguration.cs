using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class WorkOrderTaskConfiguration : IEntityTypeConfiguration<WorkOrderTask>
    {
        public void Configure(EntityTypeBuilder<WorkOrderTask> builder)
        {
            builder.ToTable("work_order_tasks");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TaskOrder).IsRequired();
            builder.Property(t => t.Description).IsRequired().HasMaxLength(1000);
            builder.Property(t => t.IsCompleted).IsRequired();
            builder.Property(t => t.CompletedAt);

            builder.HasOne(t => t.WorkOrder).WithMany(wo => wo.Tasks)
                .HasForeignKey(t => t.WorkOrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.WorkOrderId);
        }
    }
}
