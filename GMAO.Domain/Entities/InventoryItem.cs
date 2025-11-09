using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class InventoryItem : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid InventoryCategoryId { get; set; }
        public InventoryCategory Category { get; set; } = null!;
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public string? Unit { get; set; }
        public decimal QuantityInStock { get; set; }
        public decimal? ReservedQuantity { get; set; }
        public decimal AvailableQuantity => QuantityInStock - (ReservedQuantity ?? 0);
        public decimal? MinimumStockLevel { get; set; }
        public decimal? ReorderPoint { get; set; }
        public decimal? ReorderQuantity { get; set; }
        public decimal? AverageCost { get; set; }
        public string? StorageLocation { get; set; }
        public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
        public ICollection<InventorySupplier> Suppliers { get; set; } = new List<InventorySupplier>();
    }
}
