namespace GMAO.Application.Features.Auth.Queries.Login.DTOs
{
    public class LoggedUserDtoForJwt
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid TenantId { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool DefaultPasswordChanged { get; set; }
        public IList<string> AppRoles { get; set; }
        public IList<string> DomainRoles { get; set; }
        public IList<string> DomainPermissions { get; set; }
        public string Token { get; set; }
    }
}
