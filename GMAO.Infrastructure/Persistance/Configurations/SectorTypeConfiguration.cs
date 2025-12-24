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
    public class SectorTypeConfiguration : IEntityTypeConfiguration<SectorType>
    {
        public void Configure(EntityTypeBuilder<SectorType> builder)
        {
            builder.ToTable("sector_types");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(255);

            builder.HasMany(x => x.Sites).WithOne(x => x.SectorType).HasForeignKey(x => x.SectorTypeId).IsRequired(true);
            builder.Ignore(sr => sr.DomainEvents);
        }
    }
}
