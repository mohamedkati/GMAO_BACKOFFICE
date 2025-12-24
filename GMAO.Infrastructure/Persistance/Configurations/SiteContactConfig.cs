using GMAO.Domain.Entities.siteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SiteContactConfig : IEntityTypeConfiguration<SiteContact>
    {
        public void Configure(EntityTypeBuilder<SiteContact> builder)
        {
            builder.ToTable("site_contacts");
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Firstname).IsRequired().HasMaxLength(100);
            builder.Property(cc => cc.Lastname).IsRequired().HasMaxLength(100);
            builder.Property(cc => cc.Email).IsRequired().HasMaxLength(200);
            builder.Property(cc => cc.Phone).HasMaxLength(20);
            builder.Property(cc => cc.CellPhone).HasMaxLength(20);
            builder.Property(cc => cc.AvailabilityHours).HasMaxLength(100);
            builder.Property(cc => cc.PreferredContactMethod).HasConversion<int>();
            builder.Property(cc => cc.IsPrimary).IsRequired().HasDefaultValue(false);

            builder.HasOne(cc => cc.Site).WithMany(x => x.Contacts)
                .HasForeignKey(cc => cc.SiteId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cc => cc.SiteContactCategory).WithMany(x => x.Contacts).HasForeignKey(x => x.SiteContactCategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(cc => cc.SiteId);
            builder.HasIndex(cc => cc.SiteContactCategoryId);
            builder.HasIndex(cc => cc.Email);
            builder.HasIndex(cc => cc.TenantId);
            builder.HasIndex(cc => new { cc.Firstname, cc.Lastname });
            builder.Ignore(cc => cc.DomainEvents);
        }
    }
}
