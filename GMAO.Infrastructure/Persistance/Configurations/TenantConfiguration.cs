using GMAO.Domain.Entities;
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
            builder.Property(t => t.Country).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Currency).HasMaxLength(10).IsRequired();
            //builder.HasMany(t => t.Clients)
            //       .WithOne()
            //       .HasForeignKey(c => c.TenantId);
        }
    }
}
