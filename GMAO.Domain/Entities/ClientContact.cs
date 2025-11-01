using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class ClientContact : BaseAuditableEntity
    {
        public Guid ClientId { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public Guid RoleId { get; set; }
        public ClientContactRole Role { get; set; } = default!;
        public Client Client { get; set; } = default!;
    }
}