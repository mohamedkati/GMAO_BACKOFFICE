using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ResponseResult<CustomerDetailedDto>>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<CustomerDetailedDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetCustomerWithContactsAndBudgetsById(request.Id);
            if (customer == null)
                return ResponseResult<CustomerDetailedDto>.FailResult("Customer not found.");

            //var customerDto = _mapper.Map<CustomerDetailedDto>(customer);
            return ResponseResult<CustomerDetailedDto>.OkResult(customer);
        }
    }
}
