using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class WorkOrderCompletionConfiguration : IEntityTypeConfiguration<WorkOrderCompletion>
    {
        public void Configure(EntityTypeBuilder<WorkOrderCompletion> builder)
        {
            builder.ToTable("work_order_completions");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CompletedAt).IsRequired();
            builder.Property(c => c.CompletedBy).IsRequired();
            builder.Property(c => c.Status).IsRequired().HasConversion<int>();
            builder.Property(c => c.WorkPerformed).HasMaxLength(2000);
            builder.Property(c => c.Notes).HasMaxLength(2000);
            builder.Property(c => c.SignatureImageId);
            builder.Property(c => c.SignedBy).HasMaxLength(200);
            builder.Property(c => c.SignedAt);

            builder.HasOne(c => c.WorkOrder).WithOne(wo => wo.Completion)
                .HasForeignKey<WorkOrderCompletion>(c => c.WorkOrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.WorkOrderId).IsUnique();
        }
    }
}
