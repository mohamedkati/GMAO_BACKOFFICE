using FluentValidation;
using GMAO.Application.Features.sites.Commands.Units.UpdateUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Units.Validators
{
    public class UpdateUnitCommandValidator : BaseUnitCommandValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator(): base()
        {
            // Règle spécifique à Update : validation de l'ID
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant du lot est obligatoire");
        }
    }
}
