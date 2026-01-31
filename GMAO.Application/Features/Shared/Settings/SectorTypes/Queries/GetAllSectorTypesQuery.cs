using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Shared.Settings.SectorTypes.Queries
{
    public class GetAllSectorTypesQuery : IRequest<ResponseResult<IReadOnlyList<SectorTypeDto>>>
    {
        public string? Search { get; set; }
    }
}
