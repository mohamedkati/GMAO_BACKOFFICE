using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteEquipementDto
    {
        public string Reference { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        //public AssetCategoryDto Category { get; set; } = null!;
        public AssetLocation? Location { get; set; }
        public DateTime InstallationDate { get; set; }
        public AssetStatus Status { get; set; }
        public CriticalityLevel CriticalityLevel { get; set; }
        public AssetHealthStatus HealthStatus { get; set; }
        public SiteEquipementDto ParentAsset { get; set; } = null!;
        public AssetReliabilityMetrics? ReliabilityMetrics { get; set; }
    }

    public class AssetCategoryDto
    {
        public string? Code { get; init; }
        public Guid Id { get; init; }
        public string Name { get; init; }
        public AssetCategoryDto ParentCategory { get; init; }

    }
}
