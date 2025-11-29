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
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("payment_methods");
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.Name).IsRequired().HasMaxLength(100);
            builder.Property(pm => pm.Terms).IsRequired(false).HasMaxLength(500);
            builder.Property(pm => pm.DueDays).IsRequired();

        }
    }
}
