using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Reference).IsRequired().HasMaxLength(50);
            builder.Property(i => i.BillToName).IsRequired().HasMaxLength(200);
            builder.Property(i => i.BillToEmail).HasMaxLength(200);
            builder.Property(i => i.InvoiceDate).IsRequired();
            builder.Property(i => i.DueDate).IsRequired();
            builder.Property(i => i.PeriodStart);
            builder.Property(i => i.PeriodEnd);
            builder.Property(i => i.SubTotal).IsRequired().HasPrecision(18, 2);
            builder.Property(i => i.DiscountAmount).HasPrecision(18, 2);
            builder.Property(i => i.VATAmount).HasPrecision(18, 2);
            builder.Property(i => i.TotalAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(i => i.PaidAmount).HasPrecision(18, 2);
            builder.Property(i => i.Status).IsRequired().HasConversion<int>();

            builder.Ignore(i => i.RemainingAmount);
            builder.Ignore(i => i.WorkOrderIds);

            builder.OwnsOne(i => i.BillToAddress, a =>
            {
                a.Property(x => x.FirstAddressLine).HasMaxLength(200).IsRequired();
                a.Property(x => x.SecondAddressLine).HasMaxLength(200);
                a.Property(x => x.City).HasMaxLength(100).IsRequired();
                a.Property(x => x.Street).HasMaxLength(100);
                a.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
                a.Property(x => x.Country).HasMaxLength(100).IsRequired();
            });

            builder.HasOne(i => i.Customer).WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.Lines).WithOne(l => l.Invoice)
                .HasForeignKey(l => l.InvoiceId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Shares).WithOne(s => s.Invoice)
                .HasForeignKey(s => s.InvoiceId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Payments).WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(i => i.Reference).IsUnique();
            builder.HasIndex(i => i.CustomerId);
            builder.HasIndex(i => i.Status);
            builder.HasIndex(i => i.InvoiceDate);
            builder.HasIndex(i => i.DueDate);
            builder.HasIndex(i => i.TenantId);
            builder.Ignore(i => i.DomainEvents);
        }
    }

}
