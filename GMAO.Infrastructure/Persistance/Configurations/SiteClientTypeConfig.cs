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
    public class SiteClientTypeConfig : IEntityTypeConfiguration<SiteClientType>
    {
        public void Configure(EntityTypeBuilder<SiteClientType> builder)
        {
            builder.ToTable("site_client_types");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired(true).HasMaxLength(50);
            builder.HasOne(x => x.SiteCategory)
                .WithMany(x => x.SiteClientTypes)
                .HasForeignKey(x => x.SiteCategoryId);

            builder.Ignore(sr => sr.DomainEvents);
        }
    }
}
