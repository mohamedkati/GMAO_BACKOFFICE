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
    class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.RegistrationNumber)
                .HasMaxLength(150);

            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(c => c.PaymentMethod)
                .WithMany(x => x.Clients)
                .HasForeignKey(x => x.PaymentMethodId)
                .IsRequired(false);

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

            builder.HasMany(x => x.Contacts)
                .WithOne(c => c.Client)
                .HasForeignKey(C => C.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Sites)
                .WithOne(s => s.Client)
                .HasForeignKey(x => x.ClientId)
                .IsRequired();

            builder.HasMany(x => x.Events)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .IsRequired();
        }
    }
}
