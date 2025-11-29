using GMAO.Application.Helpers.Request;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.property_group.queries.ListAllPropertyGroups
{
    public class ListAllPropertyGroupsQuery : PagedRequest, IRequest<PagedResponse<ListPropertyGroupDto>>
    {
        public int[]? Type { get; set; }
        public int[]? Status { get; set; }
        public int[]? LegalForm { get; set; }
        public string? Search { get; set; }
    }
}
