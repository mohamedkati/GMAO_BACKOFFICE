using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class Unit : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public Guid SiteId { get; set; }
        public Site Site { get; set; } = null!;
        public UnitType Type { get; set; }
        public UnitStatus Status { get; set; }
        public string? Floor { get; set; }
        public string? DoorNumber { get; set; }
        public decimal? SurfaceArea { get; set; }
        public int? Rooms { get; set; }
        public int OwnershipSharesCount { get; set; }
        public ICollection<Occupant> Occupants { get; set; } = new List<Occupant>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}