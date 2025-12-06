using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.DeleteBudget
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, ResponseResult<bool>>
    {
        private readonly IRepository<MaintenanceBudget> _budgetRepo;
        public DeleteBudgetCommandHandler(IRepository<MaintenanceBudget> budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }
        public async Task<ResponseResult<bool>> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepo.GetByIdAsync(request.Id, cancellationToken);
            if (budget == null)
                return ResponseResult<bool>.FailResult("Budget not found.");

            if (budget.CustomerId != request.CustomerId)
                return ResponseResult<bool>.FailResult("Le budget n'appartient pas au client spécifié.");

            _budgetRepo.Remove(budget);
            await _budgetRepo.SaveChangesAsync(cancellationToken);

            return ResponseResult<bool>.OkResult(true, "Le budget est supprimé avec succès");
        }
    }
}
