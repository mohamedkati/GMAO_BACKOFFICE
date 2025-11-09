using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class PurchaseOrderLine : BaseAuditableEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public int LineNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal OrderedQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => OrderedQuantity * UnitPrice;
    }
}