using GMAO.Domain.Entities;
using GMAO.Domain.Entities.Auth;
using GMAO.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class StaffConfig : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs");
            builder.Property(s => s.EmployeeNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne<Address>(s => s.Address, a =>
            {
                a.Property(ad => ad.FirstAddressLine)
                    .IsRequired(false)
                    .HasMaxLength(200)
                    .HasColumnName("AddressLine1");

                a.Property(ad => ad.SecondAddressLine)
                 .IsRequired(false)
                 .HasMaxLength(200)
                 .HasColumnName("AddressLine2");
                a.Property(ad => ad.Street)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Street");
                a.Property(ad => ad.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("City");
                a.Property(ad => ad.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("PostalCode");
                a.Property(ad => ad.Country)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Country");
            });

            builder.Property(s => s.CellPhone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(150);
            //
            //  Lien entre TenantUser.UserId → Staffs(Id)
            //
            builder.Property(s => s.Status).IsRequired().HasConversion<int>();

            builder.HasMany<TenantUser>(x=> x.Tenants)
             .WithOne(x=> x.User)
             .HasForeignKey(tu => tu.StaffId)
             .OnDelete(DeleteBehavior.NoAction);

            // JSON columns for PostgreSQL
            //builder.Property(s => s.ServiceZones).HasColumnType("jsonb");
            //builder.Property(s => s.ManagedCustomerIds).HasColumnType("jsonb");

            builder.Ignore(builder => builder.ServiceZones);
            builder.Ignore(builder => builder.ManagedCustomerIds);

            builder.HasIndex(s => s.Email).IsUnique();
            builder.HasIndex(s => s.RoleId);
            builder.HasIndex(x=> x.EmployeeNumber).IsUnique();
            builder.HasIndex(s => s.Status);
            builder.HasIndex(s => s.TenantId);
            builder.Ignore(s => s.DomainEvents);

        }
    }

}
