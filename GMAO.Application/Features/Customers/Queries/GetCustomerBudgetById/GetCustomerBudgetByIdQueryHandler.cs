using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Queries.GetCustomerBudgetById
{
    public class GetCustomerBudgetByIdQueryHandler : IRequestHandler<GetCustomerBudgetByIdQuery, ResponseResult<CustomerBudgetDto>>
    {
        private readonly IRepository<MaintenanceBudget> _repoCustomerBudget;
        private readonly IMapper _mapper;

        public GetCustomerBudgetByIdQueryHandler(IRepository<MaintenanceBudget> repoCustomerBudget, IMapper mapper)
        {
            this._repoCustomerBudget = repoCustomerBudget;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<CustomerBudgetDto>> Handle(GetCustomerBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            var budget = await _repoCustomerBudget.FirstOrDefaultAsync(x => x.Id == request.BudgetId && x.CustomerId == request.CustomerId, cancellationToken);

            if (budget == null)
            {
                throw new NotFoundException("Budget", request.BudgetId);
            }

            return ResponseResult<CustomerBudgetDto>.OkResult(_mapper.Map<CustomerBudgetDto>(budget));
        }
    }
}
