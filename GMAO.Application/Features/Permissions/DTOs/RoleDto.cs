namespace GMAO.Application.Features.Permissions.DTOs
{
    // Roles
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public int Priority { get; set; }
        public int PermissionCount { get; set; }
        public int UserCount { get; set; }
    }
}