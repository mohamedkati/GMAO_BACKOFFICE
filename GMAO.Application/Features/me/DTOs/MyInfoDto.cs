namespace GMAO.Application.Features.me.DTOs
{
    public class MyInfoDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public IReadOnlyList<string> Permissions { get; set; }
        public Guid TenantId { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; } = 0;
    }
}
