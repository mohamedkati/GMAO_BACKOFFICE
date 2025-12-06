using FluentValidation;

namespace GMAO.Application.Features.Customers.Commands.CreateBudget
{
    public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
    {
        public CreateBudgetCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Le client est requis.");
            RuleFor(x => x.Year)
                .GreaterThan(DateTime.Now.Year - 1).WithMessage($"Year must be greater than {DateTime.Now.Year - 1}.")
                .LessThanOrEqualTo(DateTime.Now.Year + 10).WithMessage($"L'année doit se situer dans une fourchette raisonnable (max {DateTime.Now.Year + 10}.");
            RuleFor(x => x.BudgetedAmount).GreaterThanOrEqualTo(0).WithMessage("Le montant budgété doit être non négatif.");
            RuleFor(x => x.CommittedAmount).GreaterThanOrEqualTo(0).WithMessage("Le montant engagé doit être non négatif.");
            RuleFor(x => x.InvoicedAmount).GreaterThanOrEqualTo(0).WithMessage("Le montant facturé doit être non négatif.");
            RuleFor(x => x.AlertThreshold).InclusiveBetween(0, 100).WithMessage("Le seuil d'alerte doit être compris entre 0 et 100.");
        }
    }
}
