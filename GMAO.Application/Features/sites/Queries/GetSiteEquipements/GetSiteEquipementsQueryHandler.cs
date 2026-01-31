using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetSiteEquipements
{
    public class GetSiteEquipementsQueryHandler : IRequestHandler<GetSiteEquipementsQuery, ResponseResult<IReadOnlyList<SiteEquipementDto>>>
    {
        private readonly ISiteRepositoryAsync _siteRepository;
        public GetSiteEquipementsQueryHandler(ISiteRepositoryAsync siteRepository)
        {
            _siteRepository = siteRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<SiteEquipementDto>>> Handle(GetSiteEquipementsQuery request, CancellationToken cancellationToken)
        {
            var siteEquipements = await _siteRepository.GetSiteEquipementsBySiteIdAsync(request.SiteId, cancellationToken);
            return ResponseResult<IReadOnlyList<SiteEquipementDto>>.OkResult(siteEquipements);
        }
    }
}
