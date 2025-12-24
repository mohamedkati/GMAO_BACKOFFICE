using GMAO.Domain.Entities.siteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class WarrantyConfiguration : IEntityTypeConfiguration<Warranty>
    {
        public void Configure(EntityTypeBuilder<Warranty> builder)
        {
            builder.ToTable("warranties");
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Type).IsRequired().HasConversion<int>();
            builder.Property(w => w.ProviderName).IsRequired().HasMaxLength(200);
            builder.Property(w => w.WarrantyNumber).HasMaxLength(100);
            builder.Property(w => w.StartDate).IsRequired();
            builder.Property(w => w.EndDate).IsRequired();
            builder.Property(w => w.CoveredItems).HasMaxLength(1000);
            builder.Property(w => w.Exclusions).HasMaxLength(1000);
            builder.Property(w => w.ContactPhone).HasMaxLength(20);
            builder.Property(w => w.ContactEmail).HasMaxLength(200);
            builder.Property(w => w.ClaimsCount).IsRequired();
            builder.Property(w => w.ClaimedAmount).HasPrecision(18, 2).IsRequired();
            builder.Property(w => w.SendExpirationAlert).IsRequired();
            builder.Property(w => w.AlertDaysBefore).IsRequired();

            builder.Ignore(w => w.IsActive);

            builder.HasOne(w => w.Asset).WithMany(a => a.Warranties)
                .HasForeignKey(w => w.AssetId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(w => w.AssetId);
            builder.HasIndex(w => w.EndDate);
        }
    }
}
