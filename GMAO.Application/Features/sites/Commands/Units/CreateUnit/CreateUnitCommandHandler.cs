using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Units.CreateUnit
{
    public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, ResponseResult<bool>>
    {
        private readonly ISiteServiceAsync _siteServiceAsync;

        public CreateUnitCommandHandler(ISiteServiceAsync siteServiceAsync)
        {
            this._siteServiceAsync = siteServiceAsync;
        }
        public async Task<ResponseResult<bool>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            await _siteServiceAsync.AddUnitAsync(request, cancellationToken);
            return ResponseResult<bool>.OkResult(true);
        }
    }
}
