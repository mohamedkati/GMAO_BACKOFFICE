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

namespace GMAO.Application.Features.Customers.Queries.GetCustomerBudgetById
{
    public class GetCustomerBudgetByIdQuery : IRequest<ResponseResult<CustomerBudgetDto>>, IRequiredPermission
    {
        public Guid CustomerId { get; set; }
        public Guid BudgetId { get; set; }
        public string[] RequiredPermissions => [PermissionConfig.CombineResourceAction(Resource.CustomerContacts, StandardAction.View)];
    }
}
