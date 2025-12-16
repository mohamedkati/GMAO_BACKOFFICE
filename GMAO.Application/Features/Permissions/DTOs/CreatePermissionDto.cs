namespace GMAO.Application.Features.Permissions.DTOs
{
    public class CreatePermissionDto
    {
        public string Resource { get; set; }
        public string Action { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } = "standard";
        public bool IsDangerous { get; set; }
    }
}