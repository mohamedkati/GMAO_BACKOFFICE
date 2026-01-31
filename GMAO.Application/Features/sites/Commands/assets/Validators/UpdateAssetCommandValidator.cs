using FluentValidation;
using GMAO.Application.Features.sites.Commands.assets.UpdateAsset;

namespace GMAO.Application.Features.sites.Commands.assets.Validators
{
    public class UpdateAssetCommandValidator : BaseAssetCommandValidator<UpdateAssetCommand>
    {
        public UpdateAssetCommandValidator() : base()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant de l'équipement est obligatoire");
        }
    }
}
