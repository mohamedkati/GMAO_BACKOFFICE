using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("sites");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Reference).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Type).IsRequired().HasConversion<int>();
            builder.Property(s => s.BuildingYear);
            builder.Property(s => s.TotalArea).HasPrecision(10, 2);
            builder.Property(s => s.FloorsCount);
            builder.Property(s => s.UnitsCount);

            // Value Object: Address
            builder.OwnsOne(s => s.Address, a =>
            {
                a.Property(x => x.Street).HasMaxLength(200).IsRequired();
                a.Property(x => x.SecondAddressLine).HasMaxLength(200);
                a.Property(x => x.City).HasMaxLength(100).IsRequired();
                a.Property(x => x.FirstAddressLine).HasMaxLength(100);
                a.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
                a.Property(x => x.Country).HasMaxLength(100).IsRequired();
            });

            // Value Object: GeoCoordinates
            builder.OwnsOne(s => s.Coordinates, c =>
            {
                c.Property(x => x.Latitude).HasPrecision(10, 7);
                c.Property(x => x.Longitude).HasPrecision(10, 7);
            });

            builder.HasOne(s => s.Customer).WithMany(c => c.Sites)
                .HasForeignKey(s => s.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Units).WithOne(u => u.Site)
                .HasForeignKey(u => u.SiteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Assets).WithOne(a => a.Site)
                .HasForeignKey(a => a.SiteId).OnDelete(DeleteBehavior.Restrict);


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

            builder.HasIndex(s => s.Reference).IsUnique();
            builder.HasIndex(s => s.CustomerId);
            //builder.HasIndex(x => x.Coordinates);
            builder.HasIndex(x => x.MarketTypeId);
            builder.HasIndex(s => s.Type);
            builder.HasIndex(s => s.TenantId);
            builder.Ignore(s => s.DomainEvents);


        }
    }
}
