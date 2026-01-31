using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.SiteKeeper.Validators
{
    public abstract class BaseSiteKeeperCommandValidator<T> : AbstractValidator<T> where T : ISiteKeeperCommand
    {
        protected BaseSiteKeeperCommandValidator()
        {
            ApplyCommonRules();
        }

        private void ApplyCommonRules()
        {
            // ══════════════════════════════════════════════════════════════════
            // SITE (Obligatoire)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.SiteId)
                .NotEmpty().WithMessage("Le site est obligatoire");

            // ══════════════════════════════════════════════════════════════════
            // IDENTITÉ (Obligatoire)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Le prénom est obligatoire")
                .MaximumLength(100).WithMessage("Le prénom ne doit pas dépasser 100 caractères");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Le nom est obligatoire")
                .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères");

            // ══════════════════════════════════════════════════════════════════
            // CONTACT
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("L'adresse email n'est pas valide")
                .MaximumLength(255).WithMessage("L'email ne doit pas dépasser 255 caractères");

            RuleFor(x => x.Phone)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Le téléphone ne doit pas dépasser 20 caractères")
                .Matches(@"^[\d\s\.\-\+\(\)]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Le format du téléphone est invalide");

            RuleFor(x => x.Mobile)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Mobile))
                .WithMessage("Le mobile ne doit pas dépasser 20 caractères")
                .Matches(@"^[\d\s\.\-\+\(\)]+$").When(x => !string.IsNullOrEmpty(x.Mobile))
                .WithMessage("Le format du mobile est invalide");

            // Au moins un moyen de contact pour un gardien
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Phone) || !string.IsNullOrEmpty(x.Mobile))
                .WithMessage("Au moins un numéro de téléphone (fixe ou mobile) est requis pour un gardien");

            // ══════════════════════════════════════════════════════════════════
            // INFORMATIONS COMPLÉMENTAIRES
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.WorkingHours)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.WorkingHours))
                .WithMessage("Les horaires ne doivent pas dépasser 200 caractères");

            RuleFor(x => x.Location)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Location))
                .WithMessage("La localisation ne doit pas dépasser 200 caractères");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Notes))
                .WithMessage("Les notes ne doivent pas dépasser 1000 caractères");
        }
    }
}
