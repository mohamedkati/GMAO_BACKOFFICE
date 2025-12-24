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
    public class SiteCategoryConfig : IEntityTypeConfiguration<SiteCategory>
    {
        public void Configure(EntityTypeBuilder<SiteCategory> builder)
        {
            builder.ToTable("site_categories");
           builder.HasKey(builder => builder.Id);
            builder.Property(x=> x.Code).HasMaxLength(100).IsRequired(true);
            builder.Property(x=> x.Description).HasMaxLength(250).IsRequired(false);
            builder.Ignore(sr => sr.DomainEvents);
        }
    }
}
