namespace GMAO.Application.Features.Permissions.DTOs
{
    public class UpdateRoleDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}