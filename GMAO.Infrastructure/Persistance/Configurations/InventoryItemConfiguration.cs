using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("inventory_items");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Reference).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Description).HasMaxLength(1000);
            builder.Property(i => i.Sku).HasMaxLength(100);
            builder.Property(i => i.Barcode).HasMaxLength(100);
            builder.Property(i => i.Unit).HasMaxLength(20);
            builder.Property(i => i.QuantityInStock).IsRequired().HasPrecision(10, 2);
            builder.Property(i => i.ReservedQuantity).HasPrecision(10, 2);
            builder.Property(i => i.MinimumStockLevel).HasPrecision(10, 2);
            builder.Property(i => i.ReorderPoint).HasPrecision(10, 2);
            builder.Property(i => i.ReorderQuantity).HasPrecision(10, 2);
            builder.Property(i => i.AverageCost).HasPrecision(10, 2);
            builder.Property(i => i.StorageLocation).HasMaxLength(100);

            builder.Ignore(i => i.AvailableQuantity);

            builder.HasOne(i => i.Category).WithMany(c => c.InventoryItems)
                .HasForeignKey(i => i.InventoryCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.Transactions).WithOne(t => t.InventoryItem)
                .HasForeignKey(t => t.InventoryItemId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.Suppliers).WithOne(s => s.InventoryItem)
                .HasForeignKey(s => s.InventoryItemId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(i => i.Reference).IsUnique();
            builder.HasIndex(i => i.Sku);
            builder.HasIndex(i => i.Barcode);
            builder.HasIndex(i => i.TenantId);
            builder.Ignore(i => i.DomainEvents);
        }
    }

}
