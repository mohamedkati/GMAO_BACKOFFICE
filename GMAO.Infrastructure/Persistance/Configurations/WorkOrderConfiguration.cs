using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = GMAO.Domain.Entities.File;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.ToTable("work_orders");
            builder.HasKey(wo => wo.Id);

            builder.Property(wo => wo.Reference).IsRequired().HasMaxLength(50);
            builder.Property(wo => wo.Title).IsRequired().HasMaxLength(200);
            builder.Property(wo => wo.Description).HasMaxLength(2000);
            builder.Property(wo => wo.Type).IsRequired().HasConversion<int>();
            builder.Property(wo => wo.Scope).IsRequired().HasConversion<int>();
            builder.Property(wo => wo.Priority).IsRequired().HasConversion<int>();
            builder.Property(wo => wo.Status).IsRequired().HasConversion<int>();
            builder.Property(wo => wo.Visibility).IsRequired().HasConversion<int>();
            builder.Property(wo => wo.BlockingReason).HasMaxLength(500);
            builder.Property(wo => wo.ExpectedUnblockDate);
            builder.Property(wo => wo.ScheduledStartDate);
            builder.Property(wo => wo.ScheduledEndDate);
            builder.Property(wo => wo.StartedAt);
            builder.Property(wo => wo.CompletedAt);
            builder.Property(wo => wo.AccessRequirement).HasConversion<int>();

            builder.HasOne(wo => wo.ServiceRequest).WithMany(sr => sr.WorkOrders)
                .HasForeignKey(wo => wo.ServiceRequestId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(wo => wo.Quote).WithMany(q => q.WorkOrders)
                .HasForeignKey(wo => wo.QuoteId).OnDelete(DeleteBehavior.SetNull);
            
            builder.HasOne(wo => wo.AssignedTechnician).WithMany(t => t.AssignedWorkOrders)
                .HasForeignKey(wo => wo.AssignedTechnicianId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(wo => wo.TimeEntries).WithOne(te => te.WorkOrder)
                .HasForeignKey(te => te.WorkOrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(wo => wo.UsedParts).WithOne(up => up.WorkOrder)
                .HasForeignKey(up => up.WorkOrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(wo => wo.Tasks).WithOne(t => t.WorkOrder)
                .HasForeignKey(t => t.WorkOrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(wo => wo.Reference).IsUnique();
            builder.HasIndex(wo => wo.Status);
            builder.HasIndex(wo => wo.Priority);
            builder.HasIndex(wo => wo.AssignedTechnicianId);
            builder.HasIndex(wo => wo.ScheduledStartDate);
            builder.HasIndex(wo => wo.TenantId);
            builder.HasIndex(wo => wo.ServiceRequestId);
            builder.Ignore(wo => wo.DomainEvents);
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Description).HasMaxLength(500);
            builder.Property(w => w.Status).HasConversion<int>();
            builder.Property(w => w.Type).HasConversion<int>();
            

            builder.HasOne(x => x.Quote)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.QuoteId);

            builder.HasOne<File>(x => x.AudioFile)
                .WithOne(x => x.WorkOrderAudioFile)
                .HasForeignKey<WorkOrder>(x => x.AudioFileId)
                .IsRequired(false);

            builder.HasOne<File>(x => x.VideoFile)
               .WithOne(x => x.WorkOrderVideoFile)
               .HasForeignKey<WorkOrder>(x => x.VideoFileId)
               .IsRequired(false);

            builder.HasMany(x => x.Files)
                .WithOne(x => x.WorkOrder)
                .HasForeignKey(x => x.WorkOrderId)
                .IsRequired(false);


            builder.HasMany(x => x.Logs)
                .WithOne(x => x.WorkOrder)
                .HasForeignKey(x => x.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
