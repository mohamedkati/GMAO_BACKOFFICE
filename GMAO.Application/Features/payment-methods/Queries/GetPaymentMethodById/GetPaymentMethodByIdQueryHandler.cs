using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.payment_method;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.payment_methods.Queries.GetPaymentMethodById
{
    public class GetPaymentMethodByIdQueryHandler : IRequestHandler<GetPaymentMethodByIdQuery, ResponseResult<PaymentMethodAsKeyValueDto>>
    {
        private readonly IRepository<PaymentMethod> _repository;
        private readonly IMapper _mapper;

        public GetPaymentMethodByIdQueryHandler(IRepository<PaymentMethod> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<PaymentMethodAsKeyValueDto>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _repository.FirstOrDefaultAsync(pm => pm.Id == request.Id);

            if (paymentMethod == null)
                return ResponseResult<PaymentMethodAsKeyValueDto>.OkResult(null);

            return ResponseResult<PaymentMethodAsKeyValueDto>.OkResult(_mapper.Map<PaymentMethodAsKeyValueDto>(paymentMethod));
        }
    }
}
