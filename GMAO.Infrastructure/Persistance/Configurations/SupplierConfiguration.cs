using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("suppliers");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.CompanyRegistrationNumber).HasMaxLength(50);
            builder.Property(s => s.Phone).HasMaxLength(20);
            builder.Property(s => s.Email).HasMaxLength(200);
            builder.Property(s => s.Website).HasMaxLength(500);
            builder.Property(s => s.ContactPerson).HasMaxLength(200);
            builder.Property(s => s.Rating).HasConversion<int>();
            builder.Property(s => s.PaymentTerms).HasMaxLength(500);

            builder.OwnsOne(s => s.Address, a =>
            {
                a.Property(x => x.FirstAddressLine).HasMaxLength(200);
                a.Property(x => x.SecondAddressLine).HasMaxLength(200);
                a.Property(x => x.City).HasMaxLength(100);
                a.Property(x => x.Street).HasMaxLength(100);
                a.Property(x => x.PostalCode).HasMaxLength(20);
                a.Property(x => x.Country).HasMaxLength(100);
            });

            builder.HasMany(s => s.PurchaseOrders).WithOne(po => po.Supplier)
                .HasForeignKey(po => po.SupplierId);
            builder.HasMany(s => s.CatalogItems).WithOne(ci => ci.Supplier)
                .HasForeignKey(ci => ci.SupplierId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.Name);
            builder.HasIndex(s => s.Email);
            builder.HasIndex(s => s.TenantId);
            builder.Ignore(s => s.DomainEvents);

        }
    }
}
