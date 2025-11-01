using GMAO.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("DomainRoles", "auth");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(r => r.Permissions)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("RolePermissions", "auth"));
        }
    }
}
