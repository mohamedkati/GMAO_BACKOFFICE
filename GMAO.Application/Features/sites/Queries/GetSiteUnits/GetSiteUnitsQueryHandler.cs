using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetSiteUnits
{
    public class GetSiteUnitsQueryHandler : IRequestHandler<GetSiteUnitsQuery, ResponseResult<IReadOnlyList<SiteUnitDto>>>
    {
        private readonly ISiteRepositoryAsync _siteRepository;
        public GetSiteUnitsQueryHandler(ISiteRepositoryAsync siteRepository)
        {
            _siteRepository = siteRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<SiteUnitDto>>> Handle(GetSiteUnitsQuery request, CancellationToken cancellationToken)
        {
            var siteUnits = await _siteRepository.GetSiteUnitsBySiteIdAsync(request.SiteId, cancellationToken);
            if (siteUnits == null || !siteUnits.Any())
            {
                return ResponseResult<IReadOnlyList<SiteUnitDto>>.FailResult("No site units found for the specified site.");
            }
            return ResponseResult<IReadOnlyList<SiteUnitDto>>.OkResult(siteUnits);
        }
    }   
}
