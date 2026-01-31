using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.SiteKeeper.CreateSiteKeeper
{
    public class CreateSiteKeeperCommandHandler: IRequestHandler<CreateSiteKeeperCommand, ResponseResult<Guid>>
    {
        public async Task<ResponseResult<Guid>> Handle(CreateSiteKeeperCommand request, CancellationToken cancellationToken)
        {
            // Implementation for handling the creation of a SiteKeeper goes here.
            // This is a placeholder implementation.
            return await Task.FromResult( ResponseResult<Guid>.OkResult(Guid.NewGuid()));
        }
    }
}
