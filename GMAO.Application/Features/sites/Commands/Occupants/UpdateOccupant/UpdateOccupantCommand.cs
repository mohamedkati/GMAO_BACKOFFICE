using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Occupants.UpdateOccupant
{
    public class UpdateOccupantCommand : IRequest<ResponseResult<bool>>, IOccupantCommand
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public Guid UnitId { get; set; }
        public OccupantType Type { get; set; }
        public PersonType PersonType { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? CompanyName { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public PreferredContactMethod? PreferredContactMethod { get; set; }

        public DateTime? MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }

        public bool HasPortalAccess { get; set; }
    }
}
