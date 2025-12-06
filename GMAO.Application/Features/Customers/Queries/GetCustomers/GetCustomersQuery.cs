using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Request;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : PagedRequest,IRequest<PagedResponse<CustomerDto>>
    {
        public string? SearchString { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public Guid? CommercialId { get; set; }
        public CustomerType? Type { get; set; }
    }
}
