using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Queries.GetDetailedSiteById
{
    public class GetDetailedSiteByIdQuery : IRequest<ResponseResult<DetailedSiteDto>>
    {
        public Guid SiteId { get; set; }
    }
}
