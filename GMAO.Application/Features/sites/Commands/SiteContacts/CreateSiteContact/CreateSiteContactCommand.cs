using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.SiteContacts.CreateSiteContact
{
    public class CreateSiteContactCommand : ISiteContactCommand, IRequest<ResponseResult<Guid>>
    {
        public Guid SiteId { get; set; }
        public Guid ContactTypeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
        public string? Notes { get; set; }
        public bool IsPrimary { get; set; }
    }
}
