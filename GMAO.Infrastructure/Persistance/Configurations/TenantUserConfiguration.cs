using GMAO.Domain.Entities.Auth;
using GMAO.Infrastructure.Persistance.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class TenantUserConfiguration : IEntityTypeConfiguration<TenantUser>
    {
        public void Configure(EntityTypeBuilder<TenantUser> builder)
        {
            builder.ToTable("TenantUsers", "auth");
            builder.HasKey(tu => tu.Id);
            builder.HasOne(tu => tu.Role)
                   .WithMany()
                   .HasForeignKey(tu => tu.RoleId);
            builder.HasOne(tu => tu.Tenant)
                   .WithMany()
                   .HasForeignKey(tu => tu.TenantId);
            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(tu => tu.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
