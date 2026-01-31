using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.payment_method;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.payment_methods.Queries.GetAsKeyValue
{
    public class GetAsKeyValueQuery : IRequest<ResponseResult<IReadOnlyList<SharedPaymentMethodDto>>>
    {
        public string? Search { get; set; }
    }
}
