using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} ne doit pas etre vide")
                .NotNull().WithMessage("{PropertyName} ne doit pas etre null")
                .EmailAddress().WithMessage("{PropertyName} format est invalide");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} est requis")
                .MinimumLength(8);
        }
    }
}
