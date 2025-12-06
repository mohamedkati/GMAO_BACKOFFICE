using FluentValidation;
using FluentValidation.Validators;
using GMAO.Application.Features.Customers.Commands.CreateCustomer;
using GMAO.Domain.ValueObjects;

namespace GMAO.Application.SharedBusiness.Validators
{
    public class BillingSettingsValidator : AbstractValidator<BillingSettings>
    {
        public BillingSettingsValidator()
        {
            RuleFor(bs => bs.Mode)
                .IsInEnum().WithMessage("Le mode de facturation n'est pas valide.");

            RuleFor(RuleFor => RuleFor.AutoGenerateInvoices)
                .NotNull().WithMessage("La génération automatique des factures est requise.");

            RuleFor(bs => bs.InvoiceFrequency)
                .IsInEnum().WithMessage("La fréquence de facturation n'est pas valide.");

            RuleFor(bs => bs.SendEmailNotifications)
                .NotNull().WithMessage("L'envoi des notifications par e-mail est requis.");

        }
    }
}