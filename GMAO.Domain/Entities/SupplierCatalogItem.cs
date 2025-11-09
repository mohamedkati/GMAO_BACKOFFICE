using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class SupplierCatalogItem : BaseEntity<Guid>
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public string Sku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string? Unit { get; set; }
        public int? LeadTimeDays { get; set; }
        public decimal? MinimumOrderQuantity { get; set; }
    }
}