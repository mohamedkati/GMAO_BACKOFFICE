using FluentValidation;

namespace GMAO.Application.Features.Auth.Queries.ForgotPassword
{
    public class ForgotPasswordQueryValidator : AbstractValidator<ForgotPasswordQuery>
    {
        public ForgotPasswordQueryValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} ne doit pas etre vide")
            .NotNull().WithMessage("{PropertyName} ne doit pas etre null")
            .EmailAddress().WithMessage("{PropertyName} format est invalide");
        }
    }
}
