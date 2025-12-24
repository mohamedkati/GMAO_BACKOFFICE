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
    public class SiteKeeperConfig : IEntityTypeConfiguration<SiteKeeper>
    {
        public void Configure(EntityTypeBuilder<SiteKeeper> builder)
        {
            builder.ToTable("site_keepers");
            builder.HasKey(x=> x.Id); 
            builder.Property(s => s.Firstname)
           .HasMaxLength(50)
           .IsRequired();

            builder.Property(s => s.Lastname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(50);

            builder.Property(s => s.Phone)
                .HasMaxLength(50);

            builder.Property(s => s.CellPhone)
                .HasMaxLength(50);

            builder.HasOne(p => p.Site)
                .WithMany(p => p.SiteKeepers)
                .HasForeignKey(p => p.SiteId)
                .IsRequired();

            builder.Ignore(sr => sr.DomainEvents);

        }
    }
}
