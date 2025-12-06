using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerContacts;

public class GetCustomerContactsQuery : IRequest<ResponseResult<IReadOnlyList<CustomerContactDto>>>
{
    public Guid CustomerId { get; set; }
}

