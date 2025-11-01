using GMAO.Domain.Common;
using GMAO.Domain.Enums;

namespace GMAO.Domain.Entities
{
    public class Event : BaseAuditableEntity
    {
        public Site Site { get; set; }
        public Guid SiteId { get; set; }
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
        public Guid? AssetId { get; set; }
        public Asset Asset { get; set; }
        public Origin Origin { get; set; }
        public EventPriority Priority { get; private set; } = EventPriority.Normal;
        public File OriginFile { get; set; }
        public Guid? OriginFileId { get; set; }
        public Interlocutor Interlocutor { get; set; }
        public Guid? InterlocutorId { get; set; }
        public string OtherInterlocutor { get; set; }
        public ServiceOrderSource ServiceOrderSource { get; set; }
        public string ServiceOrderReference { get; set; }
        public File ServiceOrderFile { get; set; }
        public Guid? ServiceOrderFileId { get; set; }
        public DateTime? ServiceOrderDate { get; set; }
        public EventReason EventReason { get; set; }
        public Guid? EventReasonId { get; set; }
        public Staff QuoteFor { get; set; }
        public Guid? QuoteForId { get; set; }
        public bool WorkQuoteRequest { get; set; }
        public bool ContractQuoteRequest { get; set; }
        public Origin QuoteOrigin { get; set; }
        public bool IsAcceptedQuote { get; set; }
        public Quote AcceptedQuote { get; set; }
        public Guid? AcceptedQuoteId { get; set; }
        public ICollection<Quote> RequestedQuotes { get; set; }
        public string Comment { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
    }


}