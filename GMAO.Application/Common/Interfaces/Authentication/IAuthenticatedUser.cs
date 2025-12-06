namespace GMAO.Application.Common.Interfaces.Authentication
{
    public interface IAuthenticatedUser
    {
        Guid UserId { get; }
        Guid TenantId { get; }

        bool IsAuthenticated();
        bool IsAccountConfirmed();
    }
}
