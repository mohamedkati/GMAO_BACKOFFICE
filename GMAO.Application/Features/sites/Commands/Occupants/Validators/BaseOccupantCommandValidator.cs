using FluentValidation;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Occupants.Validators
{
    public abstract class BaseOccupantCommandValidator<T> : AbstractValidator<T> where T : IOccupantCommand
    {
        protected BaseOccupantCommandValidator()
        {
            ApplyCommonRules();
        }

        private void ApplyCommonRules()
        {
            // ══════════════════════════════════════════════════════════════════
            // LOCALISATION (Obligatoire)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.SiteId)
                .NotEmpty().WithMessage("Le site est obligatoire");

            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("Le lot est obligatoire");

            // ══════════════════════════════════════════════════════════════════
            // TYPE (Obligatoire)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Le type d'occupant est invalide");

            RuleFor(x => x.PersonType)
                .IsInEnum().WithMessage("Le type de personne est invalide");

            // ══════════════════════════════════════════════════════════════════
            // IDENTITÉ - Personne physique
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.FirstName)
                .NotEmpty().When(x => x.PersonType == PersonType.Individual)
                .WithMessage("Le prénom est obligatoire pour une personne physique")
                .MaximumLength(100).WithMessage("Le prénom ne doit pas dépasser 100 caractères");

            RuleFor(x => x.LastName)
                .NotEmpty().When(x => x.PersonType == PersonType.Individual)
                .WithMessage("Le nom est obligatoire pour une personne physique")
                .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères");

            // ══════════════════════════════════════════════════════════════════
            // IDENTITÉ - Personne morale
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.CompanyName)
                .NotEmpty().When(x => x.PersonType == PersonType.Company)
                .WithMessage("La raison sociale est obligatoire pour une entreprise")
                .MaximumLength(200).WithMessage("La raison sociale ne doit pas dépasser 200 caractères");

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

            RuleFor(x => x.PreferredContactMethod)
                .IsInEnum().When(x => x.PreferredContactMethod.HasValue)
                .WithMessage("La méthode de contact préférée est invalide");

            // Au moins un moyen de contact
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Email) ||
                           !string.IsNullOrEmpty(x.Phone) ||
                           !string.IsNullOrEmpty(x.Mobile))
                .WithMessage("Au moins un moyen de contact (email, téléphone ou mobile) est requis");

            // ══════════════════════════════════════════════════════════════════
            // PÉRIODE D'OCCUPATION
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.MoveInDate)
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(1)).When(x => x.MoveInDate.HasValue)
                .WithMessage("La date d'entrée ne peut pas être trop éloignée dans le futur (max 1 an)");

            RuleFor(x => x.MoveOutDate)
                .GreaterThan(x => x.MoveInDate).When(x => x.MoveInDate.HasValue && x.MoveOutDate.HasValue)
                .WithMessage("La date de sortie doit être postérieure à la date d'entrée");

            // ══════════════════════════════════════════════════════════════════
            // RÈGLES MÉTIER
            // ══════════════════════════════════════════════════════════════════

            // Un propriétaire ne devrait pas avoir de date de sortie prévue (sauf vente)
            RuleFor(x => x)
                .Must(x => x.Type != OccupantType.Owner || !x.MoveOutDate.HasValue ||
                           x.MoveOutDate.Value > DateTime.UtcNow.AddMonths(1))
                .WithMessage("Un propriétaire ne devrait pas avoir une date de sortie proche sauf en cas de vente");

            // Accès portail nécessite un email
            RuleFor(x => x)
                .Must(x => !x.HasPortalAccess || !string.IsNullOrEmpty(x.Email))
                .WithMessage("Un email est requis pour activer l'accès au portail");
        }
    }
}