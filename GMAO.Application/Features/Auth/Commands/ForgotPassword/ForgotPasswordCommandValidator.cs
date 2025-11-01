using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Code)
              .NotEmpty()
              .NotNull().WithMessage("Le code est requis");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotEmpty().WithMessage("Le code est requis")
                .Must(id => Guid.TryParse(id, out var converted))
                .WithMessage("Le format de l'identifiant de l'utilisateur n'est pas valide.");

            RuleFor(x => x.Password)
               .NotNull()
               .NotEmpty().WithMessage("{PropertyName} est requis")
               .MinimumLength(8);
        }
    }
}
