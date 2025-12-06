using FluentValidation;

namespace GMAO.Application.Features.Auth.Queries.EmailConfirmation
{
    class RequestEmailConfirmationQueryValidator : AbstractValidator<RequestEmailConfirmationQuery>
    {
        public RequestEmailConfirmationQueryValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("{PropertyName} ne doit pas etre vide")
              .NotNull().WithMessage("{PropertyName} ne doit pas etre null")
              .EmailAddress().WithMessage("{PropertyName} format est invalide");
        }
    }
}
