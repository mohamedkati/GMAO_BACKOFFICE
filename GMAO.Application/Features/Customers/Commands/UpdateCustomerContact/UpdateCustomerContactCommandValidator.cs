using FluentValidation;

namespace GMAO.Application.Features.Customers.Commands.UpdateCustomerContact;

public class UpdateCustomerContactCommandValidator :AbstractValidator<UpdateCustomerContactCommand>
{
    public UpdateCustomerContactCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Le prénom est obligatoire.")
            .MaximumLength(100).WithMessage("Le prénom ne peut pas dépasser 100 caractères.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Le nom de famille est obligatoire.")
            .MaximumLength(100).WithMessage("Le nom de famille ne peut pas dépasser 100 caractères.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email est obligatoire.")
            .EmailAddress().WithMessage("L'email n'est pas valide.")
            .MaximumLength(200).WithMessage("L'email ne peut pas dépasser 200 caractères.");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Le numéro de téléphone ne peut pas dépasser 20 caractères.");

        RuleFor(x => x.Mobile)
            .MaximumLength(20).WithMessage("Le numéro de mobile ne peut pas dépasser 20 caractères.");

        RuleFor(x => x.Position)
            .MaximumLength(100).WithMessage("Le poste ne peut pas dépasser 100 caractères.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Le type de personne est obligatoire.")
            .IsInEnum().WithMessage("Le type de personne n'est pas valide.");

        RuleFor(x => x.PreferredContactMethod)
            .IsInEnum().WithMessage("La méthode de contact préférée n'est pas valide.");
    }
}


