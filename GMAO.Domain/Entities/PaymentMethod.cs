using GMAO.Domain.Common;
using GMAO.Domain.Entities.siteAggregate;

namespace GMAO.Domain.Entities
{
    public class PaymentMethod : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string Terms { get; set; } = default!;
        public int Days { get; set; } = 0;
        public DueDateType TypeDueDate { get; set; } = DueDateType.Net;
        public int DueDays { get; set; } = 0;

        public ICollection<Customer> Clients { get; set; } = new List<Customer>();
        public ICollection<Site> Sites { get; set; } = new List<Site>();
    }
    
}
