using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SupplierCatalogItemConfiguration : IEntityTypeConfiguration<SupplierCatalogItem>
    {
        public void Configure(EntityTypeBuilder<SupplierCatalogItem> builder)
        {
            builder.ToTable("supplier_catalog_items");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Sku).IsRequired().HasMaxLength(100);
            builder.Property(ci => ci.Description).IsRequired().HasMaxLength(1000);
            builder.Property(ci => ci.UnitPrice).IsRequired().HasPrecision(10, 2);
            builder.Property(ci => ci.Unit).HasMaxLength(20);
            builder.Property(ci => ci.LeadTimeDays);
            builder.Property(ci => ci.MinimumOrderQuantity).HasPrecision(10, 2);

            builder.HasOne(ci => ci.Supplier).WithMany(s => s.CatalogItems)
                .HasForeignKey(ci => ci.SupplierId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ci => ci.SupplierId);
            builder.HasIndex(ci => ci.Sku);
        }
    }

}
