using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteUnitDto 
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public Guid SiteId { get; set; }
        public UnitType Type { get; set; }
        public UnitStatus Status { get; set; }
        public string? Floor { get; set; }
        public string? DoorNumber { get; set; }
        public decimal? SurfaceArea { get; set; }
        public int? Rooms { get; set; }
        public int OwnershipSharesCount { get; set; }
        public int EquipementsCount { get; set; }
        public OccupantUnitSite? ActiveOccupant { get; set; }
    }
    
}
