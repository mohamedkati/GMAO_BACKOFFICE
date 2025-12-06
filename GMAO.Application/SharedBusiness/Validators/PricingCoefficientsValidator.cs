using FluentValidation;
using GMAO.Domain.ValueObjects;

namespace GMAO.Application.SharedBusiness.Validators;

public class PricingCoefficientsValidator : AbstractValidator<PricingCoefficients>
{
    public PricingCoefficientsValidator()
    {
        RuleFor(x => x.LaborCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Le coefficient de main-d'œuvre doit être supérieure ou égale à  1.0");

        RuleFor(x => x.MaterialCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Le coefficient de matériau doit être supérieure ou égale à  1.0");

        RuleFor(x => x.EquipmentCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Le coefficient d'équipement doit être supérieure ou égale à  1.0");

        RuleFor(x => x.SubcontractorCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Le coefficient du sous-traitant doit être supérieure ou égale à  1.0");

    }
}