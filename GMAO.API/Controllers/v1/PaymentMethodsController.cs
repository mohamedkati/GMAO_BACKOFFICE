using GMAO.Application.Features.payment_methods.Queries.GetAsKeyValue;
using GMAO.Application.Features.payment_methods.Queries.GetPaymentMethodById;
using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.payment_method;

namespace GMAO.API.Controllers.v1
{

    public class PaymentMethodsController : AuthorizedController
    {
        public PaymentMethodsController()
        {
        }

        [HttpGet("get-as-key-value")]
        public async Task<ActionResult<ResponseResult<IReadOnlyList<PaymentMethodAsKeyValueDto>>>> GetPaymentMethods([FromQuery] string? search)
        {
            return Ok(await Mediator.Send(new GetAsKeyValueQuery() { Search = search }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseResult<PaymentMethodAsKeyValueDto>>> GetPaymentMethodById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetPaymentMethodByIdQuery() { Id = id }));
        }
    }
}
