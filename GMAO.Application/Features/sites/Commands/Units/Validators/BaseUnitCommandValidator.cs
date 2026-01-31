using FluentValidation;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Units.Validators
{
    public abstract class BaseUnitCommandValidator<T> : AbstractValidator<T> where T : IUnitCommand
    {
        protected BaseUnitCommandValidator()
        {
            ApplyCommonRules();
        }

        private void ApplyCommonRules()
        {
            // ══════════════════════════════════════════════════════════════════
            // INFORMATIONS DE BASE (Obligatoires)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Reference)
                .NotEmpty().WithMessage("La référence est obligatoire")
                .MaximumLength(50).WithMessage("La référence ne doit pas dépasser 50 caractères")
                .Matches(@"^[A-Za-z0-9\-_]+$").WithMessage("La référence ne peut contenir que des lettres, chiffres, tirets et underscores");

            RuleFor(x => x.SiteId)
                .NotEmpty().WithMessage("Le site est obligatoire");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Le type de lot est invalide");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Le statut du lot est invalide");

            // ══════════════════════════════════════════════════════════════════
            // LOCALISATION (Optionnels)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Floor)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Floor))
                .WithMessage("L'étage ne doit pas dépasser 20 caractères")
                .Matches(@"^[A-Za-z0-9\-\s]+$").When(x => !string.IsNullOrEmpty(x.Floor))
                .WithMessage("L'étage ne peut contenir que des lettres, chiffres, tirets et espaces");

            RuleFor(x => x.DoorNumber)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.DoorNumber))
                .WithMessage("Le numéro de porte ne doit pas dépasser 20 caractères");

            // ══════════════════════════════════════════════════════════════════
            // CARACTÉRISTIQUES PHYSIQUES
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.SurfaceArea)
                .GreaterThan(0).When(x => x.SurfaceArea.HasValue)
                .WithMessage("La surface doit être supérieure à 0")
                .LessThanOrEqualTo(100_000).When(x => x.SurfaceArea.HasValue)
                .WithMessage("La surface semble incorrecte (max 100 000 m²)");

            RuleFor(x => x.Rooms)
                .GreaterThanOrEqualTo(0).When(x => x.Rooms.HasValue)
                .WithMessage("Le nombre de pièces ne peut pas être négatif")
                .LessThanOrEqualTo(100).When(x => x.Rooms.HasValue)
                .WithMessage("Le nombre de pièces semble incorrect (max 100)");

            // ══════════════════════════════════════════════════════════════════
            // COPROPRIÉTÉ
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.OwnershipSharesCount)
                .GreaterThanOrEqualTo(0).WithMessage("Les tantièmes ne peuvent pas être négatifs")
                .LessThanOrEqualTo(100_000).WithMessage("Les tantièmes semblent incorrects (max 100 000)");

            // ══════════════════════════════════════════════════════════════════
            // RÈGLES MÉTIER - Cohérence Type/Caractéristiques
            // ══════════════════════════════════════════════════════════════════

            // Un parking ou une cave n'a généralement pas de pièces
            RuleFor(x => x)
                .Must(x => !x.Rooms.HasValue || x.Rooms == 0 ||
                           (x.Type != UnitType.Parking && x.Type != UnitType.Storage))
                .WithMessage("Un parking ou une cave ne devrait pas avoir de pièces");

            // Surface minimale selon le type
            RuleFor(x => x)
                .Must(x => !x.SurfaceArea.HasValue || x.SurfaceArea >= 5 ||
                           x.Type == UnitType.Parking || x.Type == UnitType.Storage)
                .WithMessage("La surface minimale pour un logement est de 5 m² (loi Carrez)");
        }
    }

}
