using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.ToTable("technicians");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.EmployeeNumber).HasMaxLength(50);
            builder.Property(t => t.Status).IsRequired().HasConversion<int>();

            builder.HasOne(t => t.Staff).WithOne()
                .HasForeignKey<Technician>(t => t.StaffId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.AssignedWorkOrders).WithOne(wo => wo.AssignedTechnician)
                .HasForeignKey(wo => wo.AssignedTechnicianId);
            builder.HasMany(t => t.Skills).WithOne(s => s.Technician)
                .HasForeignKey(s => s.TechnicianId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Metrics).WithOne(m => m.Technician)
                .HasForeignKey<TechnicianMetrics>(m => m.TechnicianId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.StaffId).IsUnique();
            builder.HasIndex(t => t.EmployeeNumber);
            builder.HasIndex(t => t.Status);
            builder.HasIndex(t => t.TenantId);
            builder.Ignore(t => t.DomainEvents);
        }
    }

}
