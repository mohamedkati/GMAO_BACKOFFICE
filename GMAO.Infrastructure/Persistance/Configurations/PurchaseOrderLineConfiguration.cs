using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
        {
            builder.ToTable("purchase_order_lines");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LineNumber).IsRequired();
            builder.Property(l => l.Description).IsRequired().HasMaxLength(1000);
            builder.Property(l => l.OrderedQuantity).IsRequired().HasPrecision(10, 2);
            builder.Property(l => l.ReceivedQuantity).HasPrecision(10, 2);
            builder.Property(l => l.Unit).HasMaxLength(20);
            builder.Property(l => l.UnitPrice).IsRequired().HasPrecision(10, 2);

            builder.Ignore(l => l.LineTotal);

            builder.HasOne(l => l.PurchaseOrder).WithMany(po => po.Lines)
                .HasForeignKey(l => l.PurchaseOrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(l => l.PurchaseOrderId);
        }
    }


}
