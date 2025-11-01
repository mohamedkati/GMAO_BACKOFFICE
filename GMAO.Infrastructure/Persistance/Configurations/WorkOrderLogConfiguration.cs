using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class WorkOrderLogConfiguration : IEntityTypeConfiguration<WorkOrderLog>
    {
        public void Configure(EntityTypeBuilder<WorkOrderLog> builder)
        {
            builder.ToTable("WorkOrderLogs");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Message).HasMaxLength(500);
            builder.HasOne(l => l.WorkOrder)
                   .WithMany(w => w.Logs)
                   .HasForeignKey(l => l.WorkOrderId);
        }
    }
}
