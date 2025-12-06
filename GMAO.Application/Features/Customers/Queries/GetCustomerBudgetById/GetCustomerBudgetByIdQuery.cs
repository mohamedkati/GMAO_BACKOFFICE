using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerBudgetById
{
    public class GetCustomerBudgetByIdQuery : IRequest<ResponseResult<CustomerBudgetDto>>
    {
        public Guid CustomerId { get; set; }
        public Guid BudgetId { get; set; }
    }
}
