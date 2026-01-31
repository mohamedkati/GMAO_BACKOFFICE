using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetSiteManagedTeam
{
    public class GetSiteManagedTeamQueryHandler : IRequestHandler<GetSiteManagedTeamQuery, ResponseResult<SiteTeamDto>>
    {
        private readonly ISiteRepositoryAsync _siteRepositoryAsync;

        public GetSiteManagedTeamQueryHandler(ISiteRepositoryAsync siteRepositoryAsync)
        {
            this._siteRepositoryAsync = siteRepositoryAsync;
        }
        public async Task<ResponseResult<SiteTeamDto>> Handle(GetSiteManagedTeamQuery request, CancellationToken cancellationToken)
        {
            var siteTeam = await _siteRepositoryAsync.GetSiteManagedTeamAsync(request.SiteId);
            return ResponseResult<SiteTeamDto>.OkResult(siteTeam);
        }
    }

}
