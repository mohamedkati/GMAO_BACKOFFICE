using FluentValidation;
using GMAO.Application.SharedBusiness.Validators;

namespace GMAO.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Reference)
            .NotEmpty().WithMessage("La référence est obligatoire.")
            .MaximumLength(50).WithMessage("La référence ne peut pas dépasser 50 caractères.");

        RuleFor(c => c.CompanyName)
            .NotEmpty().WithMessage("Le nom de l'entreprise est obligatoire.")
            .MaximumLength(100).WithMessage("Le nom de l'entreprise ne peut pas dépasser 100 caractères.");

        RuleFor(c => c.Siren)
            .MaximumLength(20).WithMessage("Le SIREN ne peut pas dépasser 20 caractères.");

        RuleFor(c => c.Comment)
            .MaximumLength(1500).WithMessage("Le commentaire ne peut pas dépasser 1500 caractères.");

        RuleFor(c => c.CommercialId)
            .NotEmpty().WithMessage("L'identifiant du commercial est obligatoire.");

        this.RuleFor(c => c.Type)
           .NotEmpty().WithMessage("Le type de client est requis.")
           .IsInEnum().WithMessage("Le type de client n'est pas valide.");

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