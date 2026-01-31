using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.CreateSite;

public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, ResponseResult<Guid>>
{
    private readonly ISiteServiceAsync _siteService;

    public CreateSiteCommandHandler(ISiteServiceAsync siteService)
    {
        this._siteService = siteService;
    }
    public async Task<ResponseResult<Guid>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var result = await _siteService.CreateSiteAsync(request, cancellationToken);
        return ResponseResult<Guid>.OkResult(result);
    }
}
