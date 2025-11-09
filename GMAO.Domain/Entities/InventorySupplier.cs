using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class InventorySupplier : BaseEntity<Guid>
    {
        public Guid InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; } = null!;
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public bool IsPreferred { get; set; }
        public decimal? PreferredPrice { get; set; }
        public int? LeadTimeDays { get; set; }
    }
}