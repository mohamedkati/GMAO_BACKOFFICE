using GMAO.Domain.Entities.siteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("assets");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Reference).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.IsCommonAsset).IsRequired();
            builder.Property(a => a.Manufacturer).HasMaxLength(100);
            builder.Property(a => a.Model).HasMaxLength(100);
            builder.Property(a => a.SerialNumber).HasMaxLength(100);
            builder.Property(a => a.InstallationDate).IsRequired();
            builder.Property(a => a.Status).IsRequired().HasConversion<int>();
            builder.Property(a => a.CriticalityLevel).IsRequired().HasConversion<int>();
            builder.Property(a => a.HealthStatus).IsRequired().HasConversion<int>();

            // Value Object: AssetReliabilityMetrics
            builder.OwnsOne(a => a.ReliabilityMetrics, m =>
            {
                m.Property(x => x.TotalFailures).IsRequired().HasDefaultValue(0);
                m.Property(x => x.TotalMaintenanceHours).IsRequired().HasDefaultValue(0);
                m.Property(x => x.MTBF).IsRequired().HasDefaultValue(0);
                m.Property(x => x.MTTR).IsRequired().HasDefaultValue(0);
                m.Property(x => x.AvailabilityPercent).HasPrecision(5, 2).IsRequired().HasDefaultValue(0);
                m.Property(x => x.FailuresPerYear).IsRequired().HasDefaultValue(0);
                m.Property(x => x.LastFailureDate);
                m.Property(x => x.LastMaintenanceDate);
            });

            builder.HasOne(a => a.Category).WithMany(c => c.Assets)
                .HasForeignKey(a => a.AssetCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Site).WithMany(s => s.Assets)
                .HasForeignKey(a => a.SiteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Unit).WithMany(u => u.Assets)
                .HasForeignKey(a => a.UnitId).OnDelete(DeleteBehavior.SetNull);
            //builder.HasMany(a => a.WorkOrders).WithOne(wo => wo.Asset)
            //    .HasForeignKey(wo => wo.AssetId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(a => a.MaintenancePlans).WithOne(mp => mp.Asset)
                .HasForeignKey(mp => mp.AssetId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(a => a.Warranties).WithOne(w => w.Asset)
                .HasForeignKey(w => w.AssetId).OnDelete(DeleteBehavior.Cascade);
            builder.OwnsOne(a => a.Location, l =>
            {
                l.Property(x => x.PlanDocumentId).IsRequired(false);
                l.HasOne(x => x.PlanDocument).WithOne().IsRequired();
                l.Property(x => x.XPosition).IsRequired(false);
                l.Property(x => x.YPosition).IsRequired(false);
                l.Property(x => x.LocationDescription).IsRequired(false).HasMaxLength(255);
            });




            builder.HasIndex(a => a.Reference).IsUnique();
            builder.HasIndex(a => a.SiteId);
            builder.HasIndex(a => a.Status);
            builder.HasIndex(a => a.CriticalityLevel);
            builder.HasIndex(a => a.TenantId);
            builder.HasIndex(x => x.AssetCategoryId);
            builder.HasIndex(x => x.UnitId);
            builder.Ignore(a => a.DomainEvents);

        }
    }
}
