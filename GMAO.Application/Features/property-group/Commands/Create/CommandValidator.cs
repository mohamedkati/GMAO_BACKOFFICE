using FluentValidation;
using FluentValidation.Validators;
using GMAO.Application.Features.property_group.Commands.Validators;
using GMAO.Domain.ValueObjects;

namespace GMAO.Application.Features.property_group.Commands
{
    public class CommandValidator : AbstractValidator<CreatePropertyGroupCommand>
    {
        public CommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Property Group Name is required.")
                .MaximumLength(200).WithMessage("Property Group Name must not exceed 200 characters.");

            this.RuleFor(x => x.Reference)
                .NotEmpty().WithMessage("Property Group Reference is required.")
                .MaximumLength(100).WithMessage("Property Group Reference must not exceed 100 characters.");

            this.RuleFor(x => x.MainContactEmail)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.MainContactEmail))
                .WithMessage("Main Contact Email must be a valid email address.");

            this.RuleFor(x => x.AccountingContactEmail)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.AccountingContactEmail))
                .WithMessage("Accounting Contact Email must be a valid email address.");

            this.RuleFor(x => x.VolumeDiscountPercent)
                .InclusiveBetween(0, 100).When(x => x.VolumeDiscountPercent.HasValue)
                .WithMessage("Volume Discount Percent must be between 0 and 100.");

            this.RuleFor(x => x.PaymentTermsDays)
                .GreaterThanOrEqualTo(0).WithMessage("Payment Terms Days must be non-negative.");

            this.RuleFor(x => x.CommercialNotes)
                .MaximumLength(1000).WithMessage("Commercial Notes must not exceed 1000 characters.");

            this.RuleFor(x => x.InternalNotes)
                .MaximumLength(1000).WithMessage("Internal Notes must not exceed 1000 characters.");

            this.RuleFor(x => x.FrameworkContractEndDate)
                .GreaterThan(x => x.FrameworkContractStartDate).When(x => x.FrameworkContractStartDate.HasValue && x.FrameworkContractEndDate.HasValue)
                .WithMessage("Framework Contract End Date must be after Start Date.");

            this.RuleFor(x => x.AccountingContactPhone)
                .MaximumLength(20).WithMessage("Accounting Contact Phone must not exceed 20 characters.");

            this.RuleFor(RuleFor => RuleFor.MainContactPhone)
                .MaximumLength(20).WithMessage("Main Contact Phone must not exceed 20 characters.");

            this.RuleFor(x => x.HeadquartersAddress)
                .SetValidator(validator: new AddressValidator());

            this.RuleFor(x => x.GroupPricingCoefficients)
                .SetValidator(validator: new GroupPricingCoefficientsValidator()).When(x => x.GroupPricingCoefficients != null);
        }
    }
}
