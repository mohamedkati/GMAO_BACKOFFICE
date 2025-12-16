namespace GMAO.Application.Features.Permissions.DTOs
{
    public class UpdatePermissionDto
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsDangerous { get; set; }
    }
}