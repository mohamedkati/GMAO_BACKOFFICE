using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class InvoiceShareConfiguration : IEntityTypeConfiguration<InvoiceShare>
    {
        public void Configure(EntityTypeBuilder<InvoiceShare> builder)
        {
            builder.ToTable("invoice_shares");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Tantièmes).IsRequired();
            builder.Property(s => s.SharePercent).IsRequired().HasPrecision(5, 2);
            builder.Property(s => s.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(s => s.IsNotified).IsRequired();
            builder.Property(s => s.NotifiedAt);

            builder.HasOne(s => s.Invoice).WithMany(i => i.Shares)
                .HasForeignKey(s => s.InvoiceId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Unit).WithMany()
                .HasForeignKey(s => s.UnitId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.Occupant).WithMany()
                .HasForeignKey(s => s.OccupantId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(s => s.InvoiceId);
            builder.HasIndex(s => s.UnitId);
        }
    }


}
