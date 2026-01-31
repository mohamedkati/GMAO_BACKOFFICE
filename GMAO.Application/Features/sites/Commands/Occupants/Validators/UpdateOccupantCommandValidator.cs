using FluentValidation;
using GMAO.Application.Features.sites.Commands.Occupants.UpdateOccupant;

namespace GMAO.Application.Features.sites.Commands.Occupants.Validators
{
    public class UpdateOccupantCommandValidator : BaseOccupantCommandValidator<UpdateOccupantCommand>
    {
        public UpdateOccupantCommandValidator() : base()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant de l'occupant est obligatoire");
        }
    }
}