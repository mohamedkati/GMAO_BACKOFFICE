using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class InventoryCategoryConfiguration : IEntityTypeConfiguration<InventoryCategory>
    {
        public void Configure(EntityTypeBuilder<InventoryCategory> builder)
        {
            builder.ToTable("inventory_categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.HasOne(c => c.ParentCategory).WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.InventoryItems).WithOne(i => i.Category)
                .HasForeignKey(i => i.InventoryCategoryId);

            builder.HasIndex(c => c.ParentCategoryId);
            builder.HasIndex(c => c.Code);
            builder.HasIndex(c => c.Name);
            builder.Ignore(c => c.DomainEvents);
        }
    }

}
