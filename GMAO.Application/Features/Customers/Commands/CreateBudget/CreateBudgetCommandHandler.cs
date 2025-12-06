using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.CreateBudget
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, ResponseResult<bool>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateBudgetCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ResponseResult<bool>> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var customerExist = await _customerRepository.AnyAsync(c=> c.Id == request.CustomerId, cancellationToken);
            if (!customerExist)
                return ResponseResult<bool>.FailResult("Customer not found.");

            var result = await _customerRepository.AddBudgetToCustomer(request);
            return ResponseResult<bool>.OkResult(result);
        }
    }
}
