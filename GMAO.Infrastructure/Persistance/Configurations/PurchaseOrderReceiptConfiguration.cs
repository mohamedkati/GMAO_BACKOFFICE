using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PurchaseOrderReceiptConfiguration : IEntityTypeConfiguration<PurchaseOrderReceipt>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderReceipt> builder)
        {
            builder.ToTable("purchase_order_receipts");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReceivedAt).IsRequired();
            builder.Property(r => r.ReceivedBy).IsRequired();
            builder.Property(r => r.Type).IsRequired().HasConversion<int>();
            builder.Property(r => r.DeliveryNote).HasMaxLength(200);
            builder.Property(r => r.Notes).HasMaxLength(2000);

            builder.HasOne(r => r.PurchaseOrder).WithMany(po => po.Receipts)
                .HasForeignKey(r => r.PurchaseOrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(r => r.PurchaseOrderId);
        }
    }

}
