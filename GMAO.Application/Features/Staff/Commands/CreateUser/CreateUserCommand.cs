using GMAO.Application.Common.Authorization;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Infrastructure;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GMAO.Application.Features.Staff.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string Email, string FirstName, string LastName, string PhoneNumber, Guid RoleId) : IRequest<ResponseResult<Guid>>, IRequiredPermission, ITransactionnalCommand
    {
        public string[] RequiredPermissions => [];
        public string[] RequiredRoles => ["Admin"];
        public bool MustMatchTenant => true;
        public bool RequireAuthentication => true;
    }
}
