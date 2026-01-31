using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.SiteKeeper.UpdateSiteKeeper
{
    public class UpdateSiteKeeperCommandHandler : IRequestHandler<UpdateSiteKeeperCommand, ResponseResult<bool>>
    {
        public async Task<ResponseResult<bool>> Handle(UpdateSiteKeeperCommand request, CancellationToken cancellationToken)
        {
            // Implementation for updating the site keeper goes here.
            // This is a placeholder for demonstration purposes.
            return  ResponseResult<bool>.OkResult(true);
        }
    }
}
