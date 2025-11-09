using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unit = GMAO.Domain.Entities.Unit;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("units");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Reference).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Type).IsRequired().HasConversion<int>();
            builder.Property(u => u.Status).IsRequired().HasConversion<int>();
            builder.Property(u => u.Floor).HasMaxLength(20);
            builder.Property(u => u.DoorNumber).HasMaxLength(20);
            builder.Property(u => u.SurfaceArea).HasPrecision(10, 2);
            builder.Property(u => u.Rooms);
            builder.Property(u => u.OwnershipSharesCount).IsRequired();

            builder.HasOne(u => u.Site).WithMany(s => s.Units)
                .HasForeignKey(u => u.SiteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.Occupants).WithOne(o => o.Unit)
                .HasForeignKey(o => o.UnitId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Assets).WithOne(a => a.Unit)
                .HasForeignKey(a => a.UnitId).OnDelete(DeleteBehavior.SetNull);
            //builder.HasMany(u => u.WorkOrders).WithOne(wo => wo.Unit)
            //    .HasForeignKey(wo => wo.UnitId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(u => u.Reference).IsUnique();
            builder.HasIndex(u => u.SiteId);
            builder.HasIndex(u => u.Status);
            builder.HasIndex(u => u.TenantId);
            builder.Ignore(u => u.DomainEvents);
        }
    }

}
