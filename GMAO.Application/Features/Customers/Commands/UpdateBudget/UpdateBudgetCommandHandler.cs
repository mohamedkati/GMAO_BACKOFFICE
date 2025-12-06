using AutoMapper;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.UpdateBudget
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, ResponseResult<bool>>
    {
        private readonly IRepository<MaintenanceBudget> _repoMaintenanceBudget;
        private readonly IMapper _mapper;

        public UpdateBudgetCommandHandler(IRepository<MaintenanceBudget> repoMaintenanceBudget, IMapper mapper)
        {
            this._repoMaintenanceBudget = repoMaintenanceBudget;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<bool>> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = await _repoMaintenanceBudget.GetByIdAsync(request.Id);
            if (budget == null)
                return ResponseResult<bool>.FailResult("Budget not found.");

            var budgetToUpdate = _mapper.Map(request, budget);
            await _repoMaintenanceBudget.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Mise à jour du budget réussi");
        }
    }
}
