using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Commands.DeleteCustomerContact
{
    public class DeleteCustomerContactCommand : IRequest<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
