using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetSites
{
    public class GetSitesQueryHandler : IRequestHandler<GetSitesQuery, PagedResponse<SiteListItemDto>>
    {
        private readonly ISiteRepositoryAsync _siteRepo;

        public GetSitesQueryHandler(ISiteRepositoryAsync siteRepo)
        {
            this._siteRepo = siteRepo;
        }
        public async Task<PagedResponse<SiteListItemDto>> Handle(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var (pagedSites, totalRecord) = await _siteRepo.GetPagedSitesAsync(request, cancellationToken);
            return  PagedResponse<SiteListItemDto>.Success(pagedSites!, request.Page, request.PageSize, totalRecord);
        }
    }
}
