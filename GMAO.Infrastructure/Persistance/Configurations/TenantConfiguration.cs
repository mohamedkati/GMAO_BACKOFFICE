using GMAO.Domain.Entities;
using GMAO.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(150).IsRequired();
            builder.Property(t => t.Subdomain).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Email).IsRequired(true).HasMaxLength(150);
            builder.OwnsOne<Address>(t => t.Address, (a) =>
            {
                a.Property(x=> x.FirstAddressLine).HasMaxLength(200);
                a.Property(x=> x.SecondAddressLine).HasMaxLength(200);
                a.Property(x=> x.Street).HasMaxLength(250);
                a.Property(x=> x.PostalCode).HasMaxLength(10);
                a.Property(x=> x.City).HasMaxLength(30);
                a.Property(x=> x.Country).IsRequired().HasMaxLength(30);
            });
            builder.Property(t => t.Phone).IsRequired(true).HasMaxLength(20);
            builder.Property(t=> t.Website).HasMaxLength(150);
            builder.Property(t=> t.Logo).HasMaxLength(250);
            builder.OwnsOne(t => t.TenantLimits);
            builder.OwnsOne(t => t.Settings, (s) =>
            {
                s.Property(x=> x.Currency).HasMaxLength(50);
                s.Property(x=> x.DateFormat).HasMaxLength(50);
                s.Property(x=> x.Language).HasMaxLength(50);
                s.Property(x=> x.TimeFormat).HasMaxLength(50);
                s.Property(x=> x.Timezone).HasMaxLength(50);
            });
            builder.OwnsOne(t => t.Features);
            builder.OwnsOne(t => t.Subscription);
            //builder.HasMany(t => t.Clients)
            //       .WithOne()
            //       .HasForeignKey(c => c.TenantId);
        }
    }
}
