using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Features.sites.Queries.GetSites;
using GMAO.Domain.Entities.siteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Repositories
{
    public interface ISiteRepositoryAsync : IRepository<Site>
    {
        Task<(IReadOnlyList<SiteListItemDto>, int)> GetPagedSitesAsync(
            GetSitesQuery request,
            CancellationToken cancellationToken);

        Task<DetailedSiteDto?> GetDetailedSiteByIdAsync(
            Guid siteId,
            CancellationToken cancellationToken);
        Task<IReadOnlyList<SiteUnitDto>> GetSiteUnitsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);
        Task<IReadOnlyList<SiteEquipementDto>> GetSiteEquipementsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);
        Task<IReadOnlyList<SiteDocumentDto>> GetSiteDocumentsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);
        Task<SiteTeamDto> GetSiteManagedTeamAsync(Guid siteId);
        Task<IReadOnlyList<SiteContactDto>> GetSiteContactsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);
    }
}
