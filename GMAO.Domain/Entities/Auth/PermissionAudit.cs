using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.Auth
{
    public class PermissionAudit : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? TargetUserId { get; set; } // Utilisateur modifié
        public Guid? TargetRoleId { get; set; } // Rôle modifié
        public string Action { get; set; } // "GRANT", "REVOKE", "ROLE_ASSIGNED", etc.
        public string PermissionCode { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
    }
}
