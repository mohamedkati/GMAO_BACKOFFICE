using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.SiteContacts.UpdateSiteContact
{
    public class UpdateSiteContactCommandHandler : IRequestHandler<UpdateSiteContactCommand, ResponseResult<bool>>
    {
        public async Task<ResponseResult<bool>> Handle(UpdateSiteContactCommand request, CancellationToken cancellationToken)
        {
            // Implementation for updating the site contact goes here.
            // This is a placeholder implementation.
            return await Task.FromResult( ResponseResult<bool>.OkResult(true));
        }
    }
}
