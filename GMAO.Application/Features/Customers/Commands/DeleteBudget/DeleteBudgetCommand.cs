using GMAO.Application.Common.Authorization;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Commands.DeleteBudget
{
    public class DeleteBudgetCommand : IRequest<ResponseResult<bool>>, IRequiredPermission
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public string[] RequiredPermissions => [PermissionConfig.CombineResourceAction(Resource.CustomerBudgets, StandardAction.Delete)];
    }
}
