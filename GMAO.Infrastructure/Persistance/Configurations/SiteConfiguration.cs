using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("Sites");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).HasMaxLength(150).IsRequired();
            builder.Property(s => s.Address).HasMaxLength(250);

            builder.HasMany(x => x.Clients)
                .WithOne(x => x.Site)
                .HasForeignKey(x => x.SiteId)
                .IsRequired();

            builder.HasOne(x => x.Commercial)
                .WithMany(x => x.SiteCommercials)
                .HasForeignKey(x => x.CommercialId);

            builder.HasOne(x => x.OperationsManager)
             .WithMany(x => x.OperationManagerForSites)
             .HasForeignKey(x => x.OperationsManagerId);

            builder.HasOne(x => x.Technician1)
                .WithMany(x => x.Technician1ForSites)
                .HasForeignKey(x => x.Technician1Id);

            builder.HasOne(x => x.Technician2)
                .WithMany(x => x.Technician2ForSites)
                .HasForeignKey(x => x.Technician2Id);

        }
    }
}
