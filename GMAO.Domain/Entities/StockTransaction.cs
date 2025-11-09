using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class StockTransaction : BaseAuditableEntity
    {
        public Guid InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; } = null!;
        public StockTransactionType Type { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal? UnitCost { get; set; }
        public Guid? WorkOrderId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}