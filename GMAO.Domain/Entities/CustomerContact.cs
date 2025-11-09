using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class CustomerContact : BaseAuditableEntity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public PersonType Type { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
        public bool IsPrimary { get; set; }
        public PreferredContactMethod PreferredContactMethod { get; set; }
    }
}