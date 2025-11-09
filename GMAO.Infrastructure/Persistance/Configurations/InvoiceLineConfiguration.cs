using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.ToTable("invoice_lines");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LineNumber).IsRequired();
            builder.Property(l => l.Type).IsRequired().HasConversion<int>();
            builder.Property(l => l.Description).IsRequired().HasMaxLength(1000);
            builder.Property(l => l.Quantity).IsRequired().HasPrecision(10, 2);
            builder.Property(l => l.Unit).HasMaxLength(20);
            builder.Property(l => l.UnitPrice).IsRequired().HasPrecision(10, 2);

            builder.Ignore(l => l.LineTotal);

            builder.HasOne(l => l.Invoice).WithMany(i => i.Lines)
                .HasForeignKey(l => l.InvoiceId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(l => l.InvoiceId);
        }
    }

}
