using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class UsedPart : BaseAuditableEntity
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; } = null!;
        public Guid? InventoryItemId { get; set; }
        public InventoryItem? InventoryItem { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost => Quantity * UnitCost;
    }
}