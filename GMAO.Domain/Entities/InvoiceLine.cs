using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class InvoiceLine : BaseEntity<Guid>
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public int LineNumber { get; set; }
        public InvoiceLineType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        public Guid? WorkOrderId { get; set; }
    }
}