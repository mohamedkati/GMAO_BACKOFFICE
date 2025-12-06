using FluentValidation;
using GMAO.Application.SharedBusiness.Validators;

namespace GMAO.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        this.RuleFor(c => c.CompanyName)
            .NotEmpty().WithMessage("Le nom de l'entreprise est requis..")
            .MaximumLength(200).WithMessage("Le nom de l'entreprise ne doit pas dépasser 200 caractères.");

        this.RuleFor(c => c.Reference)
            .NotEmpty().WithMessage("La référence est requise.")
            .MaximumLength(50).WithMessage("La référence ne doit pas dépasser 50 caractères.");

        this.RuleFor(c => c.Siren)
            .MaximumLength(14).WithMessage("Le SIREN ne doit pas dépasser 14 caractères.");

        this.RuleFor(c => c.CommercialId)
            .NotEmpty().WithMessage("Le commercial est requis.");

        this.RuleFor(c => c.Type)
            .NotEmpty().WithMessage("Le type de client est requis.")
            .IsInEnum().WithMessage("Le type de client n'est pas valide.");

        this.RuleFor(c => c.Comment)
            .MaximumLength(1500).WithMessage("Le commentaire ne doit pas dépasser 1500 caractères.");

        this.RuleFor(c => c.InvoiceAddress)
            .NotNull().WithMessage("L'adresse de facturation est requise.")
            .SetValidator(new AddressValidator());

        RuleFor(c => c.MailingAddress)
            .SetValidator(new AddressValidator())
            .When(c => c.MailingAddress != null);

        RuleFor(c => c.PricingCoefficients)
            .SetValidator(new PricingCoefficientsValidator());

        RuleFor(c => c.BillingSettings)
            .SetValidator(new BillingSettingsValidator());
    }
}