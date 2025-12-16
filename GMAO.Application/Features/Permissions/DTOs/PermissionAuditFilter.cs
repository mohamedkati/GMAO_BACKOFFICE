namespace GMAO.Application.Features.Permissions.DTOs
{
    public class PermissionAuditFilter
    {
        public Guid? UserId { get; set; }
        public Guid? TargetUserId { get; set; }
        public Guid? TargetRoleId { get; set; }
        public string Action { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Limit { get; set; } = 100;
    }
}