using FluentValidation;
using GMAO.Shared.RegExValidators;
using System.Text.RegularExpressions;

namespace GMAO.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .NotNull().WithMessage("Le mot de passe est requis")
                .Must(pass => !Regex.IsMatch(pass, RegexValidator.PASSWORD_VALIDATOR_REGEX))
                .WithMessage("Vous devez utiliser un mot de passe plus puissant (au minimum 8 caractères, majiscule, miniscule, caractère spéciaux)");

            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .NotNull().WithMessage("L'ancien mot de passe est obligatoire");
        }
    }
}
