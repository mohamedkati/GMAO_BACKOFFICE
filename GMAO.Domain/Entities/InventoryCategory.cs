using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class InventoryCategory : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public InventoryCategory? ParentCategory { get; set; }
        public ICollection<InventoryCategory> SubCategories { get; set; } = new List<InventoryCategory>();
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}