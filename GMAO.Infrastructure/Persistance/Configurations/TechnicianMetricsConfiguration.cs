using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class TechnicianMetricsConfiguration : IEntityTypeConfiguration<TechnicianMetrics>
    {
        public void Configure(EntityTypeBuilder<TechnicianMetrics> builder)
        {
            builder.ToTable("technician_metrics");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.TotalWorkOrders).IsRequired();
            builder.Property(m => m.CompletedWorkOrders).IsRequired();
            builder.Property(m => m.AverageCompletionTime).HasPrecision(10, 2);
            builder.Property(m => m.FirstTimeFixRate).HasPrecision(5, 2);
            builder.Property(m => m.CustomerSatisfactionScore).HasPrecision(5, 2);

            builder.HasOne(m => m.Technician).WithOne(t => t.Metrics)
                .HasForeignKey<TechnicianMetrics>(m => m.TechnicianId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.TechnicianId).IsUnique();
        }
    }

}
