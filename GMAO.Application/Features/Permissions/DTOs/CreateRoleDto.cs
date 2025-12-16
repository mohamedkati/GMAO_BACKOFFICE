namespace GMAO.Application.Features.Permissions.DTOs
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; } = 50;
    }
}