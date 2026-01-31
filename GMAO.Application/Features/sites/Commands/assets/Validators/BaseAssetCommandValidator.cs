using FluentValidation;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.assets.Validators
{
    public abstract class BaseAssetCommandValidator<T> : AbstractValidator<T> where T : IAssetCommand
    {
        protected BaseAssetCommandValidator()
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

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom de l'équipement est obligatoire")
                .MinimumLength(2).WithMessage("Le nom doit contenir au moins 2 caractères")
                .MaximumLength(200).WithMessage("Le nom ne doit pas dépasser 200 caractères");

            // ══════════════════════════════════════════════════════════════════
            // CATÉGORIE (Obligatoire)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("La catégorie est obligatoire");

            // ══════════════════════════════════════════════════════════════════
            // LOCALISATION
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.SiteId)
                .NotEmpty().WithMessage("Le site est obligatoire");

            RuleFor(x => x.UnitId)
                .NotEmpty().When(x => x.UnitId.HasValue)
                .WithMessage("L'ID du lot est invalide");

            // Cohérence IsCommonAsset / UnitId
            RuleFor(x => x)
                .Must(x => x.IsCommonAsset || x.UnitId.HasValue)
                .WithMessage("Un équipement privatif doit être associé à un lot");

            RuleFor(x => x)
                .Must(x => !x.IsCommonAsset || !x.UnitId.HasValue)
                .WithMessage("Un équipement de parties communes ne peut pas être associé à un lot");

            // Validation de la localisation
            When(x => x.Location != null, () =>
            {
                RuleFor(x => x.Location!.LocationDescription)
                    .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Location?.LocationDescription))
                    .WithMessage("La description de localisation ne doit pas dépasser 500 caractères");

                RuleFor(x => x.Location!.XPosition)
                    .InclusiveBetween(0, 10000).When(x => x.Location?.XPosition.HasValue == true)
                    .WithMessage("La position X doit être comprise entre 0 et 10000");

                RuleFor(x => x.Location!.YPosition)
                    .InclusiveBetween(0, 10000).When(x => x.Location?.YPosition.HasValue == true)
                    .WithMessage("La position Y doit être comprise entre 0 et 10000");
            });

            // ══════════════════════════════════════════════════════════════════
            // DÉTAILS TECHNIQUES
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Manufacturer)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Manufacturer))
                .WithMessage("Le fabricant ne doit pas dépasser 100 caractères");

            RuleFor(x => x.Model)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Model))
                .WithMessage("Le modèle ne doit pas dépasser 100 caractères");

            RuleFor(x => x.SerialNumber)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.SerialNumber))
                .WithMessage("Le numéro de série ne doit pas dépasser 100 caractères");

            RuleFor(x => x.InstallationDate)
                .LessThanOrEqualTo(DateTime.UtcNow.AddDays(30)).When(x => x.InstallationDate.HasValue)
                .WithMessage("La date d'installation ne peut pas être dans le futur (tolérance 30 jours)")
                .GreaterThanOrEqualTo(new DateTime(1900, 1, 1)).When(x => x.InstallationDate.HasValue)
                .WithMessage("La date d'installation semble incorrecte");

            // ══════════════════════════════════════════════════════════════════
            // STATUT ET CRITICITÉ
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Le statut de l'équipement est invalide");

            RuleFor(x => x.CriticalityLevel)
                .IsInEnum().WithMessage("Le niveau de criticité est invalide");

            RuleFor(x => x.HealthStatus)
                .IsInEnum().WithMessage("L'état de santé est invalide");

            // ══════════════════════════════════════════════════════════════════
            // HIÉRARCHIE
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.ParentAssetId)
                .NotEmpty().When(x => x.ParentAssetId.HasValue)
                .WithMessage("L'ID de l'équipement parent est invalide");

            // ══════════════════════════════════════════════════════════════════
            // RÈGLES MÉTIER - Cohérence Statut/Santé
            // ══════════════════════════════════════════════════════════════════

            // Un équipement décommissionné devrait être en état critique ou mauvais
            RuleFor(x => x)
                .Must(x => x.Status != AssetStatus.Decommissioned ||
                           x.HealthStatus == AssetHealthStatus.Poor ||
                           x.HealthStatus == AssetHealthStatus.Critical)
                .WithMessage("Un équipement décommissionné devrait avoir un état de santé 'Mauvais' ou 'Critique'");

            // Un équipement en panne doit avoir un statut approprié
            RuleFor(x => x)
                .Must(x => x.HealthStatus != AssetHealthStatus.Critical ||
                           x.Status == AssetStatus.Faulty ||
                           x.Status == AssetStatus.UnderMaintenance ||
                           x.Status == AssetStatus.Decommissioned)
                .WithMessage("Un équipement en état critique devrait être en panne, en maintenance ou décommissionné");
        }
    }
}
