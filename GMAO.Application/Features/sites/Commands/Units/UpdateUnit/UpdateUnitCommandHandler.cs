using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.Units.UpdateUnit
{
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, ResponseResult<bool>>
    {
        private readonly ISiteServiceAsync _siteServiceAsync;

        public UpdateUnitCommandHandler(ISiteServiceAsync siteServiceAsync)
        {
            this._siteServiceAsync = siteServiceAsync;
        }
        public async Task<ResponseResult<bool>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            await _siteServiceAsync.UpdateUnitAsync(request, cancellationToken);
            return ResponseResult<bool>.OkResult(true);
        }
    }
}
