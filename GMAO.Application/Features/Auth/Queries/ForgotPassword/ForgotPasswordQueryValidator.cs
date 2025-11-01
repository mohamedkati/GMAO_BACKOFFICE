using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
