using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class PaymentMethod : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string Terms { get; set; } = default!;

        public ICollection<Client> Clients { get; set; } = new List<Client>();  
    }
}
