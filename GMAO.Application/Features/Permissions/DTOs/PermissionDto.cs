namespace GMAO.Application.Features.Permissions.DTOs
{
    // Permissions
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsDangerous { get; set; }
    }
}