using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerForSelectControl
{
    public class GetCustomerForSelectControlQuery : IRequest<ResponseResult<IReadOnlyList<CustomerForSelectControlDto>>>
    {
        public string? Search { get; set; }
        public int? PageSize { get; set; }
    }

    public class GetCustomerForSelectControlQueryHandler : IRequestHandler<GetCustomerForSelectControlQuery, ResponseResult<IReadOnlyList<CustomerForSelectControlDto>>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomerForSelectControlQueryHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<CustomerForSelectControlDto>>> Handle(GetCustomerForSelectControlQuery request, CancellationToken cancellationToken)
        {
            // Implementation logic to retrieve customers based on the search criteria and page size
            // This is a placeholder for the actual data retrieval logic
            var customers = await customerRepository.GetCustomersForSelectControlAsync(request.Search, request.PageSize, cancellationToken);

            return ResponseResult<IReadOnlyList<CustomerForSelectControlDto>>.OkResult(customers);
        }
    }
}
