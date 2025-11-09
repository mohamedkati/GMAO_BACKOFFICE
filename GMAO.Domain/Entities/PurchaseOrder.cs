using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class PurchaseOrder : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public PurchaseOrderSource Source { get; set; }
        public Guid? QuoteId { get; set; }
        public Quote? Quote { get; set; }
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public string? SupplierOrderReference { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<PurchaseOrderLine> Lines { get; set; } = new List<PurchaseOrderLine>();
        public ICollection<PurchaseOrderReceipt> Receipts { get; set; } = new List<PurchaseOrderReceipt>();
        public bool IsFullyReceived => Lines.All(l => l.ReceivedQuantity >= l.OrderedQuantity);
    }
}
