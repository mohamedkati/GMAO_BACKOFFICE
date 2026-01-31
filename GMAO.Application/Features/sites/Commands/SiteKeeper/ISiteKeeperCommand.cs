using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.SiteKeeper
{
    public interface ISiteKeeperCommand
    {
        Guid SiteId { get; }
        string FirstName { get; }
        string LastName { get; }
        string? Email { get; }
        string? Phone { get; }
        string? Mobile { get; }
        string? WorkingHours { get; }
        string? Location { get; }
        string? Notes { get; }
        bool IsMainKeeper { get; }
    }
}
