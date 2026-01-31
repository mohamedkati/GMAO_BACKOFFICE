using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities.siteAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Queries.GetSiteKeepers
{
    public class GetSiteKeepersQuery : IRequest<ResponseResult<IReadOnlyList<SiteKeeperDto>>>
    {
        public Guid SiteId { get; set; }
    }
}
