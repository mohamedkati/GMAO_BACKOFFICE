using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.assets.UpdateAsset
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, ResponseResult<bool>>
    {
        private readonly ISiteServiceAsync _siteService;

        public UpdateAssetCommandHandler(ISiteServiceAsync siteService)
        {
            this._siteService = siteService;
        }
        public async Task<ResponseResult<bool>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            await _siteService.UpdateAssetAsync(request, cancellationToken);
            return ResponseResult<bool>.OkResult(true);
        }
    }
}
