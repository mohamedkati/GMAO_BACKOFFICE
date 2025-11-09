using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class AssetCategoryConfiguration : IEntityTypeConfiguration<AssetCategory>
    {
        public void Configure(EntityTypeBuilder<AssetCategory> builder)
        {
            builder.ToTable("asset_categories");
            builder.HasKey(ac => ac.Id);

            builder.Property(ac => ac.Name).IsRequired().HasMaxLength(100);
            builder.Property(ac => ac.Description).HasMaxLength(500);
            builder.Property(ac => ac.Code).HasMaxLength(20);

            builder.HasOne(ac => ac.ParentCategory).WithMany(ac => ac.SubCategories)
                .HasForeignKey(ac => ac.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(ac => ac.Assets).WithOne(a => a.Category)
                .HasForeignKey(a => a.AssetCategoryId);

            builder.HasIndex(ac => ac.Code);
            builder.HasIndex(ac => ac.ParentCategoryId);
            builder.HasIndex(ac => ac.TenantId);
            builder.Ignore(ac => ac.DomainEvents);
        }
    }
}

