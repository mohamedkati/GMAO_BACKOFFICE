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
    public class SiteDocumentConfig : IEntityTypeConfiguration<SiteDocument>
    {
        public void Configure(EntityTypeBuilder<SiteDocument> builder)
        {
            builder.ToTable("site_documents");
             builder.HasKey(e=> e.Id);
            builder.Property(x => x.IsPlan).IsRequired().HasDefaultValue(false);
            builder.Property(x=> x.Description).HasMaxLength(500).IsRequired(false);
            builder.Property(x=> x.FileName).IsRequired(true).HasMaxLength(180);
            builder.Property(x => x.FilePath).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.FileSize).IsRequired();
            builder.Property(x => x.MimeType).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.SendExpirationAlert).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x=> x.ExpirationDate).IsRequired(false);
            builder.HasOne(x => x.Site).WithMany(x => x.Documents).HasForeignKey(x => x.SiteId);
            builder.Ignore(sr => sr.DomainEvents);
        }
    }
}
