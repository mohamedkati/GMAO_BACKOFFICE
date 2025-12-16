namespace GMAO.Application.Features.Permissions.DTOs
{
    // Audit
    public class PermissionAuditDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? TargetUserId { get; set; }
        public Guid? TargetRoleId { get; set; }
        public string Action { get; set; }
        public string PermissionCode { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
    }
}