using GMAO.Domain.Common;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class Asset : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid AssetCategoryId { get; set; }
        public AssetCategory Category { get; set; } = null!;
        public Guid SiteId { get; set; }
        public Site Site { get; set; } = null!;
        public Guid? UnitId { get; set; }
        public Unit? Unit { get; set; }
        public bool IsCommonAsset { get; set; } = true;
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public AssetLocation? Location { get; set; }
        public DateTime InstallationDate { get; set; }
        public AssetStatus Status { get; set; }
        public CriticalityLevel CriticalityLevel { get; set; }
        public AssetHealthStatus HealthStatus { get; set; }
        public AssetReliabilityMetrics? ReliabilityMetrics { get; set; }
        public ICollection<MaintenancePlan> MaintenancePlans { get; set; } = new List<MaintenancePlan>();
        public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
        public Guid? ParentAssetId { get; set; }
        public Asset? ParentAsset { get; set; }
        public ICollection<Customer> ClientsCommercial { get; set; } = new List<Customer>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    }
}
