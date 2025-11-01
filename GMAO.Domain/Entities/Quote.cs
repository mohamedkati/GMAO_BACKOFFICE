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
        public decimal TotalAmount { get; set; }
        public DateTime ValidUntil { get; set; }
        public QuoteStatus Status { get; set; } = QuoteStatus.Draft;
        public ICollection<QuoteLine> Lines { get; set; } = new List<QuoteLine>();
        public void Accept() => Status = QuoteStatus.Accepted;
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}
