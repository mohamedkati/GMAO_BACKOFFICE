using FluentValidation;
using GMAO.Application.Features.sites.Commands.SiteContacts.UpdateSiteContact;

namespace GMAO.Application.Features.sites.Commands.SiteContacts.Validators
{
    public class UpdateSiteContactCommandValidator : BaseSiteContactCommandValidator<UpdateSiteContactCommand>
    {
        public UpdateSiteContactCommandValidator() : base()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant du contact est obligatoire");
        }
    }
}
