using GMAO.Application.Common.Authorization;
using GMAO.Application.Common.Interfaces.Infrastructure;
using GMAO.Application.Helpers.Responses;
using MediatR;
namespace GMAO.Application.Features.Staffs.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string Email, string FirstName, string LastName, string PhoneNumber, Guid RoleId) : IRequest<ResponseResult<Guid>>, IRequiredPermission, ITransactionnalCommand
    {
        public string[] RequiredPermissions => [];
        public string[] RequiredRoles => ["Admin"];
        public bool MustMatchTenant => true;
        public bool RequireAuthentication => true;
    }
}
