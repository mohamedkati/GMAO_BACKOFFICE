using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Shared.Settings.Vat.Queries.GetAllVats
{
    public class GetAllVatQuery : IRequest<ResponseResult<IReadOnlyList<TVATypeDto>>>
    {
        public string? Search { get; set; }
    }
}
