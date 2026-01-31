using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.SiteContacts
{
    public interface ISiteContactCommand
    {
        Guid SiteId { get; }
        Guid ContactTypeId { get; }
        string FirstName { get; }
        string LastName { get; }
        string? Email { get; }
        string? Phone { get; }
        string? Mobile { get; }
        string? Position { get; }
        string? Notes { get; }
        bool IsPrimary { get; }
    }
}
