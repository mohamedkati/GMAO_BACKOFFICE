using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Assets");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(150).IsRequired();
            builder.Property(a => a.SerialNumber).HasMaxLength(100);
            builder.HasOne(a => a.Site)
                   .WithMany(s => s.Assets)
                   .HasForeignKey(a => a.SiteId);
            builder.HasOne(a => a.AssetType)
                   .WithMany()
                   .HasForeignKey(a => a.AssetTypeId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(a => a.ParentAsset)
                   .WithMany()
                   .HasForeignKey(a => a.ParentAssetId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
