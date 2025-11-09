using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class AssetCategory : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public AssetCategory? ParentCategory { get; set; }
        public ICollection<AssetCategory> SubCategories { get; set; } = new List<AssetCategory>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
