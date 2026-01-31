using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Validators
{
    public abstract class BaseSiteCommnadValidator<T> : AbstractValidator<T> where T : ISiteCommand
    {
        protected BaseSiteCommnadValidator()
        {
            // ══════════════════════════════════════════════════════════════════
            // INFORMATIONS DE BASE (Obligatoires)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Reference)
                .NotEmpty().WithMessage("La référence est obligatoire")
                .MaximumLength(50).WithMessage("La référence ne doit pas dépasser 50 caractères")
                .Matches(@"^[A-Za-z0-9\-_]+$").WithMessage("La référence ne peut contenir que des lettres, chiffres, tirets et underscores");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom du site est obligatoire")
                .MinimumLength(2).WithMessage("Le nom doit contenir au moins 2 caractères")
                .MaximumLength(200).WithMessage("Le nom ne doit pas dépasser 200 caractères");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Le type de site est invalide");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Le client est obligatoire");

            // ══════════════════════════════════════════════════════════════════
            // RÉFÉRENCES (Optionnelles mais validées si présentes)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.SectorTypeId)
                .NotEmpty().WithMessage("L'ID du type de secteur est invalide")
                .NotNull().WithMessage("L'ID du type de secteur est invalide");

            RuleFor(x => x.ClientTypeId)
                .NotEmpty().WithMessage("L'ID du type de client est invalide")
                .NotNull().WithMessage("L'ID du type de client est invalide");

            RuleFor(x => x.VatId)
                .NotEmpty().WithMessage("L'ID de la TVA est invalide")
                .NotNull().WithMessage("L'ID de la TVA est invalide");

            RuleFor(x => x.PaymentMethodId)
                .NotEmpty().When(x => x.PaymentMethodId.HasValue)
                .WithMessage("L'ID du mode de paiement est invalide");

            // ══════════════════════════════════════════════════════════════════
            // ADRESSE PRINCIPALE (Obligatoire)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Address)
                .NotNull().WithMessage("L'adresse est obligatoire");

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.Street)
                    .NotEmpty().WithMessage("La rue est obligatoire")
                    .MaximumLength(200).WithMessage("La rue ne doit pas dépasser 200 caractères");

                RuleFor(x => x.Address.City)
                    .NotEmpty().WithMessage("La ville est obligatoire")
                    .MaximumLength(100).WithMessage("La ville ne doit pas dépasser 100 caractères");

                RuleFor(x => x.Address.PostalCode)
                    .NotEmpty().WithMessage("Le code postal est obligatoire")
                    .MaximumLength(10).WithMessage("Le code postal ne doit pas dépasser 10 caractères")
                    .Matches(@"^\d{5}$").When(x => x.Address?.Country == "France")
                    .WithMessage("Le code postal français doit contenir 5 chiffres");

                RuleFor(x => x.Address.Country)
                    .NotEmpty().WithMessage("Le pays est obligatoire")
                    .MaximumLength(100).WithMessage("Le pays ne doit pas dépasser 100 caractères");
            });

            // ══════════════════════════════════════════════════════════════════
            // ADRESSE DE FACTURATION (Optionnelle)
            // ══════════════════════════════════════════════════════════════════

            When(x => x.BillingAddress != null, () =>
            {
                RuleFor(x => x.BillingAddress!.Street)
                    .NotEmpty().WithMessage("La rue de facturation est obligatoire si l'adresse est fournie")
                    .MaximumLength(200).WithMessage("La rue de facturation ne doit pas dépasser 200 caractères");

                RuleFor(x => x.BillingAddress!.City)
                    .NotEmpty().WithMessage("La ville de facturation est obligatoire")
                    .MaximumLength(100).WithMessage("La ville de facturation ne doit pas dépasser 100 caractères");

                RuleFor(x => x.BillingAddress!.PostalCode)
                    .NotEmpty().WithMessage("Le code postal de facturation est obligatoire")
                    .MaximumLength(10).WithMessage("Le code postal de facturation ne doit pas dépasser 10 caractères");

                RuleFor(x => x.BillingAddress!.Country)
                    .NotEmpty().WithMessage("Le pays de facturation est obligatoire")
                    .MaximumLength(100).WithMessage("Le pays de facturation ne doit pas dépasser 100 caractères");
            });

            // ══════════════════════════════════════════════════════════════════
            // COORDONNÉES GPS (Optionnelles)
            // ══════════════════════════════════════════════════════════════════

            When(x => x.Coordinates != null, () =>
            {
                RuleFor(x => x.Coordinates!.Latitude)
                    .InclusiveBetween(-90, 90).WithMessage("La latitude doit être comprise entre -90 et 90");

                RuleFor(x => x.Coordinates!.Longitude)
                    .InclusiveBetween(-180, 180).WithMessage("La longitude doit être comprise entre -180 et 180");
            });

            // ══════════════════════════════════════════════════════════════════
            // CARACTÉRISTIQUES DU BÂTIMENT
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.BuildingYear)
                .InclusiveBetween(1800, DateTime.UtcNow.Year + 5)
                .When(x => x.BuildingYear.HasValue)
                .WithMessage($"L'année de construction doit être comprise entre 1800 et {DateTime.UtcNow.Year + 5}");

            RuleFor(x => x.TotalArea)
                .GreaterThan(0).When(x => x.TotalArea.HasValue)
                .WithMessage("La surface totale doit être supérieure à 0")
                .LessThanOrEqualTo(10_000_000).When(x => x.TotalArea.HasValue)
                .WithMessage("La surface totale semble incorrecte (max 10 000 000 m²)");

            RuleFor(x => x.FloorsCount)
                .InclusiveBetween(-10, 200).When(x => x.FloorsCount.HasValue)
                .WithMessage("Le nombre d'étages doit être compris entre -10 (sous-sols) et 200");

            RuleFor(x => x.UnitsCount)
                .GreaterThanOrEqualTo(0).When(x => x.UnitsCount.HasValue)
                .WithMessage("Le nombre de lots ne peut pas être négatif")
                .LessThanOrEqualTo(10_000).When(x => x.UnitsCount.HasValue)
                .WithMessage("Le nombre de lots semble incorrect (max 10 000)");

            // ══════════════════════════════════════════════════════════════════
            // INFORMATIONS LÉGALES
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Siren)
                .Matches(@"^\d{9}$").When(x => !string.IsNullOrEmpty(x.Siren))
                .WithMessage("Le SIREN doit contenir exactement 9 chiffres");

            RuleFor(x => x.Siret)
                .Matches(@"^\d{14}$").When(x => !string.IsNullOrEmpty(x.Siret))
                .WithMessage("Le SIRET doit contenir exactement 14 chiffres");

            // Cohérence SIREN/SIRET
            RuleFor(x => x)
                .Must(x => string.IsNullOrEmpty(x.Siren) || string.IsNullOrEmpty(x.Siret) || x.Siret.StartsWith(x.Siren))
                .When(x => !string.IsNullOrEmpty(x.Siren) && !string.IsNullOrEmpty(x.Siret))
                .WithMessage("Le SIRET doit commencer par le SIREN");

            // ══════════════════════════════════════════════════════════════════
            // EMAILS
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.MainMailAddress)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.MainMailAddress))
                .WithMessage("L'adresse email principale n'est pas valide")
                .MaximumLength(255).WithMessage("L'email principal ne doit pas dépasser 255 caractères");

            RuleFor(x => x.InvoiceMailAddress)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.InvoiceMailAddress))
                .WithMessage("L'adresse email de facturation n'est pas valide")
                .MaximumLength(255).WithMessage("L'email de facturation ne doit pas dépasser 255 caractères");

            // ══════════════════════════════════════════════════════════════════
            // COMMENTAIRES
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.Comment)
                .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Comment))
                .WithMessage("Le commentaire ne doit pas dépasser 2000 caractères");

            RuleFor(x => x.CommentReport)
                .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.CommentReport))
                .WithMessage("Le commentaire rapport ne doit pas dépasser 2000 caractères");

            // ══════════════════════════════════════════════════════════════════
            // ÉQUIPE ASSIGNÉE (Optionnelle)
            // ══════════════════════════════════════════════════════════════════

            RuleFor(x => x.CommercialId)
                .NotEmpty().When(x => x.CommercialId.HasValue)
                .WithMessage("L'ID du commercial est invalide");

            RuleFor(x => x.OperationsManagerId)
                .NotEmpty().When(x => x.OperationsManagerId.HasValue)
                .WithMessage("L'ID du responsable d'exploitation est invalide");

            RuleFor(x => x.SectorManagerId)
                .NotEmpty().When(x => x.SectorManagerId.HasValue)
                .WithMessage("L'ID du chef de secteur est invalide");

            RuleFor(x => x.Technician1Id)
                .NotEmpty().When(x => x.Technician1Id.HasValue)
                .WithMessage("L'ID du technicien principal est invalide");

            RuleFor(x => x.Technician2Id)
                .NotEmpty().When(x => x.Technician2Id.HasValue)
                .WithMessage("L'ID du technicien secondaire est invalide");

            // Éviter les doublons dans l'équipe
            RuleFor(x => x)
                .Must(x => x.Technician1Id != x.Technician2Id || !x.Technician1Id.HasValue)
                .WithMessage("Le technicien principal et secondaire doivent être différents");

            // ══════════════════════════════════════════════════════════════════
            // INFORMATIONS D'ACCÈS AU SITE (Optionnelles)
            // ══════════════════════════════════════════════════════════════════

            When(x => x.SiteAccessInfo != null, () =>
            {
                RuleFor(x => x.SiteAccessInfo!.AccessCodes)
                    .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.AccessCodes))
                    .WithMessage("Les codes d'accès ne doivent pas dépasser 500 caractères");

                RuleFor(x => x.SiteAccessInfo!.KeyInstructions)
                    .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.KeyInstructions))
                    .WithMessage("Les instructions clés ne doivent pas dépasser 500 caractères");

                RuleFor(x => x.SiteAccessInfo!.WorkingHours)
                    .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.WorkingHours))
                    .WithMessage("Les horaires ne doivent pas dépasser 200 caractères");

                RuleFor(x => x.SiteAccessInfo!.AccessRestrictions)
                    .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.AccessRestrictions))
                    .WithMessage("Les restrictions d'accès ne doivent pas dépasser 500 caractères");

                RuleFor(x => x.SiteAccessInfo!.SafetyRequirements)
                    .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.SafetyRequirements))
                    .WithMessage("Les exigences de sécurité ne doivent pas dépasser 500 caractères");

                RuleFor(x => x.SiteAccessInfo!.ParkingInfo)
                    .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.ParkingInfo))
                    .WithMessage("Les informations de parking ne doivent pas dépasser 300 caractères");

                RuleFor(x => x.SiteAccessInfo!.GeneralInstructions)
                    .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.SiteAccessInfo?.GeneralInstructions))
                    .WithMessage("Les instructions générales ne doivent pas dépasser 1000 caractères");
            });
        }
    }
}
