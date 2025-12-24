using GMAO.Application.Common.Authorization;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Request;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Authorization;
using GMAO.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : PagedRequest, IRequest<PagedResponse<CustomerDto>>, IRequiredPermission
    {
        public string? SearchString { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public Guid? CommercialId { get; set; }
        public List<CustomerType>? Types { get; set; }
        public string? City { get; set; }
        // Tri
        public string? SortBy { get; set; } = "companyName";
        public string? SortOrder { get; set; } = "asc";

        public string[] RequiredPermissions => [PermissionConfig.CombineResourceAction(Resource.Customers, StandardAction.View)];
    }
}
