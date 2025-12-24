using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class Occupant : BaseAuditableEntity
    {
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public OccupantType Type { get; set; }
        public PersonType PersonType { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public DateTime? MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public bool HasPortalAccess { get; set; }
        public PreferredContactMethod? PreferredContactMethod { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}