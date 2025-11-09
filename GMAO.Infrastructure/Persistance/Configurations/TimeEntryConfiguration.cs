using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntry>
    {
        public void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            builder.ToTable("time_entries");
            builder.HasKey(te => te.Id);

            builder.Property(te => te.Type).IsRequired().HasConversion<int>();
            builder.Property(te => te.StartTime).IsRequired();
            builder.Property(te => te.EndTime);
            builder.Property(te => te.DurationMinutes).IsRequired();
            builder.Property(te => te.Description).HasMaxLength(1000);
            builder.Property(te => te.IsBillable).IsRequired();
            builder.Property(te => te.HourlyRate).HasPrecision(10, 2);

            builder.HasOne(te => te.WorkOrder).WithMany(wo => wo.TimeEntries)
                .HasForeignKey(te => te.WorkOrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(te => te.Technician).WithMany()
                .HasForeignKey(te => te.TechnicianId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(te => te.WorkOrderId);
            builder.HasIndex(te => te.TechnicianId);
            builder.HasIndex(te => te.StartTime);
        }
    }
}
