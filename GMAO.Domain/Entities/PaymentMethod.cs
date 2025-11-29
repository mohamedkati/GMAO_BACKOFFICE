using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class PaymentMethod : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string Terms { get; set; } = default!;
        public int DueDays { get; set; }

        public ICollection<Customer> Clients { get; set; } = new List<Customer>();
    }
}
