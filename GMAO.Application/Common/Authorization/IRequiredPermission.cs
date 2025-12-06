namespace GMAO.Application.Common.Authorization
{
    public interface IRequiredPermission
    {
        string[] RequiredPermissions { get; }
        string[] RequiredRoles { get; }
        bool MustMatchTenant { get; }
        bool RequireAuthentication { get; }
        bool CheckEmailConfirmedAndPasswordChanged => true;
    }
}
