using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Features.sites.Queries.GetSites;
using GMAO.Domain.Entities;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GMAO.Infrastructure.Persistance.Repositories
{
    public class SiteRepositoryAsync : Repository<Site>, ISiteRepositoryAsync
    {
        private readonly IAuthenticatedUser _authenticatedUser;

        public SiteRepositoryAsync(AppDbContext context, IMapper mapper, IAuthenticatedUser authenticatedUser) : base(context, mapper)
        {
            this._authenticatedUser = authenticatedUser;
        }

        #region search and list sites with pagination, filtering, and sorting
        public async Task<(IReadOnlyList<SiteListItemDto>, int)> GetPagedSitesAsync(GetSitesQuery request, CancellationToken cancellationToken)
        {
            var query = _entityTable
                .Include(x => x.Customer)
                .Include(x => x.Units)
                .Include(x => x.Commercial)
                .AsNoTracking()
                .AsQueryable();

            query = ApplyFilter(query, request);
            query = ApplySorting(query, request);

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var pagedSites = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<SiteListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return (pagedSites, totalCount);
        }


        private IQueryable<Site> ApplyFilter(IQueryable<Site> query, GetSitesQuery request)
        {
            // Apply filters based on the request
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(site =>
                    site.Name.Contains(request.Search) ||
                    site.Reference.Contains(request.Search) ||
                    site.Address.PostalCode.Contains(request.Search) ||
                    site.Customer.CompanyName.Contains(request.Search));
            }
            if (request.CustomerId.HasValue)
            {
                query = query.Where(site => site.CustomerId == request.CustomerId.Value);
            }

            if (!string.IsNullOrEmpty(request.Type))
            {
                if (Enum.TryParse<SiteType>(request.Type, out var siteType))
                {
                    query = query.Where(site => site.Type == siteType);
                }
            }
            if (!string.IsNullOrEmpty(request.City))
            {
                query = query.Where(site => site.Address.City.Contains(request.City));
            }
            if (request.CommercialId.HasValue)
            {
                query = query.Where(site => site.CommercialId == request.CommercialId.Value);
            }
            if (request.SectorManagerId.HasValue)
            {
                query = query.Where(site => site.SectorManagerId == request.SectorManagerId.Value);
            }

            if (request.OperationManagerId.HasValue)
            {
                query = query.Where(site => site.OperationsManagerId == request.OperationManagerId.Value);
            }
            if (request.SectorTypeId.HasValue)
            {
                query = query.Where(site => site.SectorTypeId == request.SectorTypeId.Value);
            }

            if (request.ClientTypeId.HasValue)
            {
                query = query.Where(site => site.ClientTypeId == request.ClientTypeId.Value);
            }

            return query;
        }

        private IQueryable<Site> ApplySorting(IQueryable<Site> query, GetSitesQuery request)
        {
            if (request.SortBy != null)
            {
                bool ascending = request.SortOrder?.ToLower() != "desc";
                query = request.SortBy.ToLower() switch
                {
                    "name" => ascending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
                    "reference" => ascending ? query.OrderBy(s => s.Reference) : query.OrderByDescending(s => s.Reference),
                    "city" => ascending ? query.OrderBy(s => s.Address.City) : query.OrderByDescending(s => s.Address.City),
                    "createdat" => ascending ? query.OrderBy(s => s.CreatedAt) : query.OrderByDescending(s => s.CreatedAt),
                    _ => query
                };
            }
            else
            {
                query = query.OrderBy(s => s.Name);
            }
            return query;
        }

        #endregion

        public async Task<DetailedSiteDto?> GetDetailedSiteByIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            var site = await _entityTable
               .Include(x => x.Customer)
               .Include(x => x.Commercial)
               .Include(x => x.PaymentMethod)
               .Include(x => x.ClientContact)
               .Include(s => s.SectorType)
               .Include(s => s.VAT)
               .Include(s => s.ClientType)
               .ThenInclude(ct => ct.SiteCategory)
               .Where(site => site.Id == siteId)
               .ProjectTo<DetailedSiteDto>(_mapper.ConfigurationProvider)
               .AsNoTracking()
               .FirstOrDefaultAsync(cancellationToken);

            if (site == null)
            {
                return null;
            }

            site.UnitsCount = await _context.Units.CountAsync(u => u.SiteId == siteId, cancellationToken);
            site.EquipementsCount = await _context.Assets.CountAsync(a => a.SiteId == siteId, cancellationToken);
            return site;
        }
        public async Task<IReadOnlyList<SiteUnitDto>> GetSiteUnitsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            var siteUnits = await _context.Units
               .Include(y => y.Occupants)
               .Where(u => u.SiteId == siteId)
               .ProjectTo<SiteUnitDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return siteUnits;
        }
        public async Task<IReadOnlyList<SiteEquipementDto>> GetSiteEquipementsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            var siteEquipements = await _context.Assets
                .Include(x => x.Category)
                .ThenInclude(x => x.ParentCategory)
                .Include(x => x.ParentAsset)
               .Where(a => a.SiteId == siteId)
               .ProjectTo<SiteEquipementDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return siteEquipements;
        }


        public async Task<IReadOnlyList<SiteDocumentDto>> GetSiteDocumentsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            var siteDocuments = await _context.SiteDocuments
               .Where(d => d.SiteId == siteId)
               .ProjectTo<SiteDocumentDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return siteDocuments;
        }

        public async Task<SiteTeamDto> GetSiteManagedTeamAsync(Guid siteId)
        {
            var team = await _entityTable
                .Where(s => s.Id == siteId)
                .Select(s => new
                {
                    Commercial = s.Commercial,
                    OperationsManager = s.OperationsManager,
                    SectorManager = s.SectorManager,
                    Technician1 = s.Technician1,
                    Technician2 = s.Technician2
                })
                .FirstOrDefaultAsync();

            return new SiteTeamDto
            {
                Commercial = await GetStaffInfo(team?.Commercial),
                OperationsManager = await GetStaffInfo(team?.OperationsManager),
                SectorManager = await GetStaffInfo(team?.SectorManager),
                Technician1 = await GetStaffInfo(team?.Technician1),
                Technician2 = await GetStaffInfo(team?.Technician2)
            };
        }

        private async Task<SiteStaffDto> GetStaffInfo(Staff? staff)
        {
            if (staff == null)
                return null;
            var roles = await _context.TenantUsers
                .Include(x => x.Role)
                .Where(x => x.StaffId == staff.Id && _authenticatedUser.TenantId == x.TenantId)
                .Select(x => x.Role)
                .AsNoTracking()
                .ToListAsync();

            var staffDto = _mapper.Map<SiteStaffDto>(staff);
            staffDto.Role = string.Join(',', roles.Select(x => x.Name));
            staffDto.Responsibilities = string.Join(", ", roles.Select(x => x.Responsibilities));
            return staffDto;
        }

        public async Task<IReadOnlyList<SiteContactDto>> GetSiteContactsBySiteIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            var contacts = await _context.SiteContacts.Include(c => c.SiteContactCategory)
                .Where(c => c.SiteId == siteId)
                .ProjectTo<SiteContactDto>(_mapper.ConfigurationProvider)
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
            return contacts;
        }
    }
}
