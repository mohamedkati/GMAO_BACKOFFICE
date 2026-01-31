using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetDetailedSiteById
{
    public class GetDetailedSiteByIdQueryHandler : IRequestHandler<GetDetailedSiteByIdQuery, ResponseResult<DetailedSiteDto>>
    {
        private readonly ISiteRepositoryAsync _siteRepo;

        public GetDetailedSiteByIdQueryHandler(ISiteRepositoryAsync siteRepo)
        {
            this._siteRepo = siteRepo;
        }
        public async Task<ResponseResult<DetailedSiteDto>> Handle(GetDetailedSiteByIdQuery request, CancellationToken cancellationToken)
        {
            var site = await _siteRepo.GetDetailedSiteByIdAsync(request.SiteId, cancellationToken);
            if (site is null) throw new NotFoundException(nameof(site), request.SiteId);

            return ResponseResult<DetailedSiteDto>.OkResult(site);
        }
    }
}
