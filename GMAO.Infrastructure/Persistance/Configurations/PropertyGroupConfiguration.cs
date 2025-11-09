using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PropertyGroupConfiguration : IEntityTypeConfiguration<PropertyGroup>
    {
        public void Configure(EntityTypeBuilder<PropertyGroup> builder)
        {
            builder.ToTable("property_groups");
            builder.HasKey(pg => pg.Id);

            builder.Property(pg => pg.Name).IsRequired().HasMaxLength(200);
            builder.Property(pg => pg.Description).HasMaxLength(500);

            builder.HasMany(pg => pg.Customers).WithOne(c => c.PropertyGroup)
                .HasForeignKey(c => c.PropertyGroupId);

            builder.HasIndex(pg => pg.TenantId);
            builder.Ignore(pg => pg.DomainEvents);
        }
    }
}
