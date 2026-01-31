
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.sites.Queries.GetSiteDocuments
{
    public class GetSiteDocumentsQueryHandler : IRequestHandler<GetSiteDocumentsQuery, ResponseResult<IReadOnlyList<SiteDocumentDto>>>
    {
        private readonly ISiteRepositoryAsync _siteRepository;

        public GetSiteDocumentsQueryHandler(ISiteRepositoryAsync siteRepository)
        {
            this._siteRepository = siteRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<SiteDocumentDto>>> Handle(GetSiteDocumentsQuery request, CancellationToken cancellationToken)
        {
            var siteDocuments = await _siteRepository.GetSiteDocumentsBySiteIdAsync(request.SiteId, cancellationToken);

            return ResponseResult<IReadOnlyList<SiteDocumentDto>>.OkResult(siteDocuments);
        }
    }

}
