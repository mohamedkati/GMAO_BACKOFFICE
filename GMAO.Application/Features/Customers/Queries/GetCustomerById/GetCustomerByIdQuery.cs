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

namespace GMAO.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<ResponseResult<CustomerDetailedDto>>, IRequiredPermission
    {
        public Guid Id { get; set; }
        public string[] RequiredPermissions => [PermissionConfig.CombineResourceAction(Resource.Customers, StandardAction.ViewDetails)];
    }
}
