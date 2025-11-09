using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class InventorySupplierConfiguration : IEntityTypeConfiguration<InventorySupplier>
    {
        public void Configure(EntityTypeBuilder<InventorySupplier> builder)
        {
            builder.ToTable("inventory_suppliers");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.IsPreferred).IsRequired();
            builder.Property(s => s.PreferredPrice).HasPrecision(10, 2);
            builder.Property(s => s.LeadTimeDays);

            builder.HasOne(s => s.InventoryItem).WithMany(i => i.Suppliers)
                .HasForeignKey(s => s.InventoryItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Supplier).WithMany()
                .HasForeignKey(s => s.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.InventoryItemId);
            builder.HasIndex(s => s.SupplierId);
        }
    }

}
