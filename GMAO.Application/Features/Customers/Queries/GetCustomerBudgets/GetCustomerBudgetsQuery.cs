using GMAO.Application.Common.Authorization;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerBudgets
{
    public class GetCustomerBudgetsQuery : IRequest<ResponseResult<IReadOnlyList<CustomerBudgetDto>>>, IRequiredPermission
    {
        public Guid CustomerId { get; set; }

        public string[] RequiredPermissions => [PermissionConfig.CombineResourceAction(Resource.CustomerBudgets,StandardAction.View)];
    }
}
