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
            builder.Property(p => p.Code).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(300);
        }
    }
}
