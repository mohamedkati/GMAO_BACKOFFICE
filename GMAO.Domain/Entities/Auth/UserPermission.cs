using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.Auth
{
    public class UserPermission : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
        public bool IsGranted { get; set; } // true = ajout, false = retrait

        // Relations
        public Staff User { get; set; }
        public Permission Permission { get; set; }
    }
}
