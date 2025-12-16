namespace GMAO.Application.Common.Authorization
{
    public interface IRequiredPermission
    {
        string[] RequiredPermissions { get; }
        string[] RequiredRoles => [];
        bool MustMatchTenant  => true;
        bool RequireAuthentication => true;
        bool CheckEmailConfirmedAndPasswordChanged => true;
    }
}
