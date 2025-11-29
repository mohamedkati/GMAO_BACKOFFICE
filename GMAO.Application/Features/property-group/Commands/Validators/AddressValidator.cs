using FluentValidation;
using FluentValidation.Validators;
using GMAO.Domain.ValueObjects;

namespace GMAO.Application.Features.property_group.Commands.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        this.RuleFor(x => x.Street)
            //.NotNull().WithMessage("Street is required.")
            .NotEmpty().WithName(nameof(Address.Street).ToLower()).WithMessage("Street must not be empty.")
            .MaximumLength(200).WithName(nameof(Address.Street).ToLower()).WithMessage("Street must not exceed 200 characters.");
            

        this.RuleFor(x => x.City)
            //.NotNull().WithMessage("City is required.")
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

        this.RuleFor(x => x.PostalCode)
            //.NotNull().WithMessage("Postal Code is required.")
            .NotEmpty().WithMessage("Postal Code is required.")
            .MaximumLength(20).WithMessage("Postal Code must not exceed 20 characters.");

        this.RuleFor(x => x.Country)
            //.NotNull().WithMessage("City is required.")
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");

    }
}