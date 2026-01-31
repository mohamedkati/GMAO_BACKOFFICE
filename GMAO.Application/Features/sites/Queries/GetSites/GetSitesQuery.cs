using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Request;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Queries.GetSites
{
    public class GetSitesQuery : PagedRequest, IRequest<PagedResponse<SiteListItemDto>> 
    {
        public string? Search { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CommercialId { get; set; }
        public Guid? SectorManagerId { get; set; }
        public Guid? OperationManagerId { get; set; }
        public Guid? SectorTypeId { get; set; }
        public Guid? ClientTypeId { get; set; }
        public string? Type { get; set; }
        public string? City { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } // "asc" | "desc"
    }
}
