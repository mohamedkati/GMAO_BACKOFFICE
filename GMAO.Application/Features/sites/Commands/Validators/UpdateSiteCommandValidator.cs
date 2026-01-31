using FluentValidation;
using GMAO.Application.Features.sites.Commands.UpdateSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Validators
{
    public class UpdateSiteCommandValidator : BaseSiteCommnadValidator<UpdateSiteCommand>
    {
        public UpdateSiteCommandValidator() : base()
        {
            // Règle spécifique à Update : validation de l'ID
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant du site est obligatoire");
        }
    }
}
