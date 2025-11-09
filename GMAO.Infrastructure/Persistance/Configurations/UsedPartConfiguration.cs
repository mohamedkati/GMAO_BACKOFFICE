using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class UsedPartConfiguration : IEntityTypeConfiguration<UsedPart>
    {
        public void Configure(EntityTypeBuilder<UsedPart> builder)
        {
            builder.ToTable("used_parts");
            builder.HasKey(up => up.Id);

            builder.Property(up => up.Description).IsRequired().HasMaxLength(500);
            builder.Property(up => up.Quantity).IsRequired().HasPrecision(10, 2);
            builder.Property(up => up.Unit).HasMaxLength(20);
            builder.Property(up => up.UnitCost).IsRequired().HasPrecision(10, 2);

            builder.Ignore(up => up.TotalCost);

            builder.HasOne(up => up.WorkOrder).WithMany(wo => wo.UsedParts)
                .HasForeignKey(up => up.WorkOrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(up => up.InventoryItem).WithMany()
                .HasForeignKey(up => up.InventoryItemId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(up => up.WorkOrderId);
        }
    }
}
