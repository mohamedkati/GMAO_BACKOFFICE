using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class CustomerContactConfiguration : IEntityTypeConfiguration<CustomerContact>
    {
        public void Configure(EntityTypeBuilder<CustomerContact> builder)
        {
            builder.ToTable("customer_contacts");
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Type).IsRequired().HasConversion<int>();
            builder.Property(cc => cc.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(cc => cc.LastName).IsRequired().HasMaxLength(100);
            builder.Property(cc => cc.Email).IsRequired().HasMaxLength(200);
            builder.Property(cc => cc.Phone).HasMaxLength(20);
            builder.Property(cc => cc.Mobile).HasMaxLength(20);
            builder.Property(cc => cc.Position).HasMaxLength(100);
            builder.Property(cc => cc.PreferredContactMethod).HasConversion<int>();

            builder.HasOne(cc => cc.Customer).WithMany(x => x.Contacts)
                .HasForeignKey(cc => cc.CustomerId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cc => cc.CustomerId);
            builder.HasIndex(cc => cc.Email);
            builder.HasIndex(cc => cc.TenantId);
            builder.HasIndex(cc => new { cc.FirstName, cc.LastName });
            builder.Ignore(cc => cc.DomainEvents);
        }
    }

}
