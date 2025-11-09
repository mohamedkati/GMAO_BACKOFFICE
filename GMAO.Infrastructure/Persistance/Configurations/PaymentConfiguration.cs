using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentDate).IsRequired();
            builder.Property(p => p.Amount).IsRequired().HasPrecision(18, 2);
            //builder.Property(p => p.Method).IsRequired().HasConversion<int>();
            builder.Property(p => p.Reference).HasMaxLength(100);
            builder.Property(p => p.Notes).HasMaxLength(1000);

            builder.HasOne(p => p.Method)
                .WithMany()
                .HasForeignKey(p => p.PaymentMethodId);

            builder.HasOne(p => p.Invoice).WithMany(i => i.Payments)
                .HasForeignKey(p => p.InvoiceId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.InvoiceId);
            builder.HasIndex(p => p.PaymentDate);
        }
    }


}
