using GMAO.Domain.Common;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Quote : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public Guid? QuoteRequestId { get; set; }
        public Guid? ServiceRequestId { get; set; }
        public ServiceRequest? ServiceRequest { get; set; } = null!;
        public QuoteRequest? QuoteRequest { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Terms { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsExpired => DateTime.UtcNow > this.ValidUntil;
        public QuoteStatus Status { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? ViewedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public int Version { get; set; } = 1;
        public Guid? PreviousVersionId { get; set; }
        public ICollection<QuoteLine> Lines { get; set; } = new List<QuoteLine>();
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public void Accept() => Status = QuoteStatus.Accepted;
    }
}
