using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.payment_method;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.payment_methods.Queries.GetAsKeyValue
{
    public class GetAsKeyValueQueryHandler : IRequestHandler<GetAsKeyValueQuery, ResponseResult<IReadOnlyList<SharedPaymentMethodDto>>>
    {
        private readonly IRepository<PaymentMethod> _repository;
        private readonly IMapper _mapper;

        public GetAsKeyValueQueryHandler(IRepository<PaymentMethod> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<IReadOnlyList<SharedPaymentMethodDto>>> Handle(GetAsKeyValueQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<PaymentMethod> result = null;
            if (!string.IsNullOrEmpty(request.Search))
                result = await _repository.FilterAsync(x => x.Name.Contains(request.Search), cancellationToken);
            else
                result = await _repository.GetAllAsync(cancellationToken);

            return ResponseResult<IReadOnlyList<SharedPaymentMethodDto>>.OkResult(_mapper.Map<IReadOnlyList<SharedPaymentMethodDto>>(result));
        }
    }
}
