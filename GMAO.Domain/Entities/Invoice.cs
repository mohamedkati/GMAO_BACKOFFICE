using GMAO.Domain.Common;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Invoice : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public string BillToName { get; set; } = string.Empty;
        public string? BillToEmail { get; set; }
        public Address BillToAddress { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount => TotalAmount - PaidAmount;
        public InvoiceStatus Status { get; set; }
        private readonly List<Guid> _workOrderIds = new();
        public IReadOnlyCollection<Guid> WorkOrderIds => _workOrderIds.AsReadOnly();
        public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
        public ICollection<InvoiceShare> Shares { get; set; } = new List<InvoiceShare>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
