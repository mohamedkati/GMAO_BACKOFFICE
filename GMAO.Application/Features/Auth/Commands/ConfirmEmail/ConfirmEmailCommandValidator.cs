using FluentValidation;

namespace GMAO.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull().WithMessage("Le code est requis");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotEmpty().WithMessage("Le code est requis")
                .Must(id => Guid.TryParse(id, out var converted))
                .WithMessage("Le format de l'identifiant de l'utilisateur n'est pas valide.");
        }
    }
}
