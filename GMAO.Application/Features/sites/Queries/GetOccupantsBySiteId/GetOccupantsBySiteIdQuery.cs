using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Queries.GetOccupantsBySiteId
{
    public class GetOccupantsBySiteIdQuery : IRequest<ResponseResult<IReadOnlyList<OccupantUnitSite>>>
    {
        public Guid SiteId { get; set; }
    }
}
