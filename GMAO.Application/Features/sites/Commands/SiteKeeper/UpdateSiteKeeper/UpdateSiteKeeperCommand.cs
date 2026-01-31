using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.SiteKeeper.UpdateSiteKeeper
{
    public class UpdateSiteKeeperCommand : ISiteKeeperCommand, IRequest<ResponseResult<bool>>   
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? WorkingHours { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public bool IsMainKeeper { get; set; }
    }
}
