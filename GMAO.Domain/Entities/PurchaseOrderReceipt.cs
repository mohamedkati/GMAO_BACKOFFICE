using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class PurchaseOrderReceipt : BaseAuditableEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public DateTime ReceivedAt { get; set; }
        public Guid ReceivedBy { get; set; }
        public ReceiptType Type { get; set; }
        public string? DeliveryNote { get; set; }
        public string? Notes { get; set; }
    }
}