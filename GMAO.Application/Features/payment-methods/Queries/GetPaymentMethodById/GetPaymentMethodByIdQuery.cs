using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.payment_method;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.payment_methods.Queries.GetPaymentMethodById
{
    public class GetPaymentMethodByIdQuery  : IRequest<ResponseResult<PaymentMethodAsKeyValueDto>>
    {
        public Guid Id { get; set; }
    }
}
