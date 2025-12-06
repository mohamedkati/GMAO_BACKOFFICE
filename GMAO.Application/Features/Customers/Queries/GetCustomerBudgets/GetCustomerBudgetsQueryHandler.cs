using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerBudgets
{
    public class GetCustomerBudgetsQueryHandler : IRequestHandler<GetCustomerBudgetsQuery, ResponseResult<IReadOnlyList<CustomerBudgetDto>>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerBudgetsQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<CustomerBudgetDto>>> Handle(GetCustomerBudgetsQuery request, CancellationToken cancellationToken)
        {
            var budgets = await _customerRepository.GetCustomerBudgetsAsync(request.CustomerId);
            return ResponseResult<IReadOnlyList<CustomerBudgetDto>>.OkResult(budgets);
        }
    }
}
