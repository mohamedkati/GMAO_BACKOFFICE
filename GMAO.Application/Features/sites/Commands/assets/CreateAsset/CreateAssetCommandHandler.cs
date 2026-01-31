using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.assets.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, ResponseResult<Guid>>
    {
        private readonly ISiteServiceAsync _siteService;

        public CreateAssetCommandHandler(ISiteServiceAsync siteService)
        {
            this._siteService = siteService;
        }
        public async Task<ResponseResult<Guid>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await _siteService.AddAssetAsync(request, cancellationToken);
            return ResponseResult<Guid>.OkResult(result);
        }
    }
}
