using GMAO.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PermissionAuditConfiguration : IEntityTypeConfiguration<PermissionAudit>
    {
        public void Configure(EntityTypeBuilder<PermissionAudit> builder)
        {
            builder.ToTable("permission_audits", "auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Action).IsRequired().HasMaxLength(50);
            builder.Property(e => e.PermissionCode).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Details).HasMaxLength(250);
            builder.Property(e => e.Timestamp).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(e => e.IpAddress).HasMaxLength(45);

        }
    }
}
