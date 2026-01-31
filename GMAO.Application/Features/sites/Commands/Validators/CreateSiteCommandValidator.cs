using FluentValidation;
using GMAO.Application.Features.sites.Commands.CreateSite;

namespace GMAO.Application.Features.sites.Commands.Validators;

public class CreateSiteCommandValidator : BaseSiteCommnadValidator<CreateSiteCommand>
{
    public CreateSiteCommandValidator() : base()
    {

    }
}
