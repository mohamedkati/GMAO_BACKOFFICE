using FluentValidation;
using FluentValidation.Validators;
using GMAO.Domain.ValueObjects;

namespace GMAO.Application.Features.property_group.Commands.Validators;

public class GroupPricingCoefficientsValidator : AbstractValidator<GroupPricingCoefficients>
{
    public GroupPricingCoefficientsValidator()
    {
        this.RuleFor(x => x.LaborCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Labor Coefficient must be >= 1.0");

        this.RuleFor(x => x.MaterialCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Material Coefficient must be >= 1.0");

        this.RuleFor(x => x.EquipmentCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Equipment Coefficient must be >= 1.0");

        this.RuleFor(x => x.SubcontractorCoefficient)
            .GreaterThanOrEqualTo(1.0m).WithMessage("Subcontractor Coefficient must be >= 1.0");

        this.RuleFor(x => x.VolumeDiscountPercent)
            .InclusiveBetween(0m, 100m).WithMessage("Volume Discount Percent must be between 0 and 100");
    }
}