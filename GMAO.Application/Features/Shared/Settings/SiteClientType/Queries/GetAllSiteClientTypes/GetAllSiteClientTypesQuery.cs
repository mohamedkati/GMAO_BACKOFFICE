using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Shared.Settings.SiteClientType.Queries.GetAllSiteClientTypes
{
    public class GetAllSiteClientTypesQuery : IRequest<ResponseResult<IReadOnlyList<SiteClientTypeDto>>>
    {
        public string? Search { get; set; }
    }
}
