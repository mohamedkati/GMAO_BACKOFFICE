using FluentValidation;
using GMAO.Application.Features.sites.Commands.SiteKeeper.UpdateSiteKeeper;

namespace GMAO.Application.Features.sites.Commands.SiteKeeper.Validators
{
    public class UpdateSiteKeeperCommandValidator : BaseSiteKeeperCommandValidator<UpdateSiteKeeperCommand>
    {
        public UpdateSiteKeeperCommandValidator() : base()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant du gardien est obligatoire");
        }
    }
}
