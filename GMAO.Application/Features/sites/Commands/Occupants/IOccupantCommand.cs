using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Occupants
{
    public interface IOccupantCommand
    {
        Guid SiteId { get; }
        Guid UnitId { get; }
        OccupantType Type { get; }
        PersonType PersonType { get; }

        // Identité
        string FirstName { get; }
        string LastName { get; }
        string? CompanyName { get; }

        // Contact
        string? Email { get; }
        string? Phone { get; }
        string? Mobile { get; }
        PreferredContactMethod? PreferredContactMethod { get; }

        // Période d'occupation
        DateTime? MoveInDate { get; }
        DateTime? MoveOutDate { get; }

        // Accès
        bool HasPortalAccess { get; }
    }
}
