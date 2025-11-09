using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("purchase_orders");
            builder.HasKey(po => po.Id);

            builder.Property(po => po.Reference).IsRequired().HasMaxLength(50);
            builder.Property(po => po.Source).IsRequired().HasConversion<int>();
            builder.Property(po => po.SupplierOrderReference).HasMaxLength(100);
            builder.Property(po => po.OrderDate).IsRequired();
            builder.Property(po => po.ExpectedDeliveryDate).IsRequired();
            builder.Property(po => po.ActualDeliveryDate);
            builder.Property(po => po.Status).IsRequired().HasConversion<int>();
            builder.Property(po => po.SubTotal).IsRequired().HasPrecision(18, 2);
            builder.Property(po => po.ShippingCost).HasPrecision(18, 2);
            builder.Property(po => po.VATAmount).HasPrecision(18, 2);
            builder.Property(po => po.TotalAmount).IsRequired().HasPrecision(18, 2);

            builder.Ignore(po => po.IsFullyReceived);

            builder.HasOne(po => po.Quote).WithMany()
                .HasForeignKey(po => po.QuoteId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(po => po.Supplier).WithMany(s => s.PurchaseOrders)
                .HasForeignKey(po => po.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(po => po.Lines).WithOne(l => l.PurchaseOrder)
                .HasForeignKey(l => l.PurchaseOrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(po => po.Receipts).WithOne(r => r.PurchaseOrder)
                .HasForeignKey(r => r.PurchaseOrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(po => po.WorkOrder).WithMany(x => x.PurchaseOrders)
                .HasForeignKey(wo => wo.WorkOrderId);

            builder.HasIndex(po => po.Reference).IsUnique();
            builder.HasIndex(po => po.SupplierId);
            builder.HasIndex(po => po.Status);
            builder.HasIndex(po => po.ExpectedDeliveryDate);
            builder.HasIndex(po => po.TenantId);
            builder.Ignore(po => po.DomainEvents);
        }
    }


}
