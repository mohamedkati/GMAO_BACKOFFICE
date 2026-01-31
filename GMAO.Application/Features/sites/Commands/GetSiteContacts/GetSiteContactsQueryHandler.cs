using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Commands.GetSiteContacts
{
    public class GetSiteContactsQueryHandler : IRequestHandler<GetSiteContactsQuery, ResponseResult<IReadOnlyList<SiteContactDto>>>
    {
        private readonly ISiteRepositoryAsync siteRepository;

        public GetSiteContactsQueryHandler(ISiteRepositoryAsync siteRepository)
        {
            this.siteRepository = siteRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<SiteContactDto>>> Handle(GetSiteContactsQuery request, CancellationToken cancellationToken)
        {
           var result = await siteRepository.GetSiteContactsBySiteIdAsync(request.SiteId, cancellationToken);
            return ResponseResult<IReadOnlyList<SiteContactDto>>.OkResult( result);
        }
    }
}
