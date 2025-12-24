using GMAO.Domain.Common;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;

namespace GMAO.Domain.Entities
{
    public class ServiceRequest : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public RequestType Type { get; set; }
        public ServiceRequestOriginType OriginType { get; set; }
        public RequestChannel Channel { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public Guid SiteId { get; set; }
        public Site Site { get; set; } = null!;
        public Guid? UnitId { get; set; }
        public Unit? Unit { get; set; }
        public Guid? AssetId { get; set; }
        public Asset? Asset { get; set; }
        public Guid? OccupantId { get; set; }
        public Occupant? Occupant { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EventPriority Urgency { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcknowledgedAt { get; set; }
        public DateTime? SLADeadline { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

        public Interlocutor Interlocutor { get; set; }
        public EventReason EventReason { get; set; }
        public Guid? EventReasonId { get; set; }
        public Staff? QuoteFor { get; set; }
        public Quote? Quote { get; set; }
        public Guid? QuoteForId { get; set; }
        public bool WorkQuoteRequest { get; set; }
        public bool ContractQuoteRequest { get; set; }
        public bool IsAcceptedQuote { get; set; }
        public string Comment { get; set; }
    }


}