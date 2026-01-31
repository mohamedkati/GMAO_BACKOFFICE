using GMAO.Application.Features.sites.Commands.assets.CreateAsset;

namespace GMAO.Application.Features.sites.Commands.assets.Validators
{
    public class CreateAssetCommandValidator : BaseAssetCommandValidator<CreateAssetCommand>
    {
        public CreateAssetCommandValidator() : base()
        {
            // Pas de règles supplémentaires spécifiques à Create
        }
    }
}
