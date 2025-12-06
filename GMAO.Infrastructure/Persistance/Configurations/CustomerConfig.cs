using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Configurations
{

    class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Reference).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Type).IsRequired().HasConversion<int>();

            // Value Object: PricingCoefficients
            builder.OwnsOne(c => c.PricingCoefficients, p =>
            {
                p.Property(x => x.LaborCoefficient).HasPrecision(5, 2).IsRequired().HasDefaultValue(1);
                p.Property(x => x.MaterialCoefficient).HasPrecision(5, 2).IsRequired().HasDefaultValue(1);
                p.Property(x => x.EquipmentCoefficient).HasPrecision(5, 2).IsRequired().HasDefaultValue(1);
                p.Property(x => x.SubcontractorCoefficient).HasPrecision(5, 2).IsRequired().HasDefaultValue(1);
            });

            // Value Object: BillingSettings
            builder.OwnsOne(c => c.BillingSettings, b =>
            {
                b.Property(x => x.Mode).HasConversion<int>().IsRequired();
                //b.Property(x => x.PaymentTermsDays).IsRequired();
                b.Property(x => x.AutoGenerateInvoices).IsRequired().HasDefaultValue(false);
                b.Property(x => x.InvoiceFrequency).HasConversion<int>().IsRequired();
                b.Property(x => x.SendEmailNotifications).IsRequired().HasDefaultValue(true);
                b.Property(x => x.ApplyLatePaymentFees).IsRequired().HasDefaultValue(false);
                b.Property(x => x.LatePaymentFeePercent).HasPrecision(5, 2);
            });

            // Relations
            builder.HasOne(c => c.PropertyGroup).WithMany(pg => pg.Customers)
                .HasForeignKey(c => c.PropertyGroupId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(c => c.Sites).WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Contracts).WithOne(sc => sc.Customer)
                .HasForeignKey(sc => sc.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Invoices).WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.Reference).IsUnique();
            builder.HasIndex(c => c.TenantId);
            builder.HasIndex(c => c.Type);
            builder.Ignore(c => c.DomainEvents);


            builder.Property(c => c.Siren)
                    .HasMaxLength(150);

            builder.HasOne(c => c.Commercial)
                .WithMany(x => x.ClientCommercials)
                .HasForeignKey(x => x.CommercialId)
                .IsRequired();

            builder.OwnsOne(x => x.InvoiceAddress, a =>
            {
                a.Property(ad => ad.Street)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("InvoiceStreet");
                a.Property(ad => ad.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("InvoiceCity");
                a.Property(ad => ad.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("InvoicePostalCode");
                a.Property(ad => ad.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("InvoiceCountry");
                a.Property(ad => ad.FirstAddressLine)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("InvoiceFirstAddressLine");
                a.Property(ad => ad.SecondAddressLine)
                    .HasMaxLength(200)
                    .HasColumnName("InvoiceSecondAddressLine");
            });

            builder.OwnsOne(x => x.MailingAddress, a =>
            {
                a.Property(ad => ad.Street)
                    .IsRequired(false)
                    .HasMaxLength(250)
                    .HasColumnName("MailingAddressStreet");
                a.Property(ad => ad.City)
                    .IsRequired(false)
                    .HasMaxLength(50)
                    .HasColumnName("MailingAddressCity");
                a.Property(ad => ad.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("MailingAddressPostalCode");
                a.Property(ad => ad.Country)
                    .IsRequired(false)
                    .HasMaxLength(50)
                    .HasColumnName("MailingAddressCountry");
                a.Property(ad => ad.FirstAddressLine)
                    .IsRequired(false)
                    .HasMaxLength(200)
                    .HasColumnName("MailingFirstAddressLine");
                a.Property(ad => ad.SecondAddressLine)
                    .HasMaxLength(200)
                    .HasColumnName("MailingSecondAddressLine");
            });


            builder.Property(c => c.Comment)
                .HasMaxLength(1500);


            builder.HasOne(c => c.PaymentMethod)
                .WithMany(c => c.Clients)
                .HasForeignKey(c => c.PaymentMethodId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
