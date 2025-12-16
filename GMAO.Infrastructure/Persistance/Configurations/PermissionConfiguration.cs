using GMAO.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "auth");
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.Code).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(300);
            //builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Resource).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Action).IsRequired().HasMaxLength(50);
            builder.Ignore(e => e.Code);
            builder.HasMany(builder => builder.Roles)
                   .WithOne(rp => rp.Permission)
                   .HasForeignKey(rp => rp.PermissionId);
            //.UsingEntity(j => j.ToTable("RolePermissions", "auth"));
            //builder.HasIndex(e => e.Code).IsUnique();
        }
    }
}
