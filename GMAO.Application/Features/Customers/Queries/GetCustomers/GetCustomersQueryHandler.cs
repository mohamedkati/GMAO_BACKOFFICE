using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedResponse<CustomerDto>>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomersQueryHandler(ICustomerRepository repository)
        {
            this._repository = repository;
        }
        public async Task<PagedResponse<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var (customers, totalRecords) = await _repository.GetFilteredCustomersAsync(request);
            return PagedResponse<CustomerDto>.Success(customers, request.Page, request.PageSize, totalRecords);
        }
    }
}
