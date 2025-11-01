using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Asset : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string SerialNumber { get; set; } = default!;
        public Guid SiteId { get; set; }
        public Guid? AssetTypeId { get; set; }
        public Guid? ParentAssetId { get; set; }
        public Site Site { get; set; } = default!;
        public AssetType? AssetType { get; set; }
        public Asset? ParentAsset { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public ICollection<Client> ClientsCommercial { get; set; } = new List<Client>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
