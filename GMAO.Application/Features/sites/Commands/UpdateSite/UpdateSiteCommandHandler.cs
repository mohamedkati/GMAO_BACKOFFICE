using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.UpdateSite
{
    public class UpdateSiteCommandHandler : IRequestHandler<UpdateSiteCommand, ResponseResult<bool>>
    {
        private readonly ISiteServiceAsync _siteService;

        public UpdateSiteCommandHandler(ISiteServiceAsync siteService)
        {
            this._siteService = siteService;
        }
        public async Task<ResponseResult<bool>> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
        {
           var result = await _siteService.UpdateSiteAsync(request, cancellationToken);
            return ResponseResult<bool>.OkResult(result);
        }
    }
}
