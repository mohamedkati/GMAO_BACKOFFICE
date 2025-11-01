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
            builder.ToTable("WorkOrders");
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Description).HasMaxLength(500);
            builder.Property(w => w.Status).HasConversion<int>();
            builder.Property(w => w.Type).HasConversion<int>();
            

            builder.HasOne(x => x.Quote)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.QuoteId);

            builder.HasOne(x => x.Event)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.EventId)
                .IsRequired();

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
