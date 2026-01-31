using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.DTOs
{
    public class OccupantUnitSite 
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public OccupantType Type { get; set; }
        public PersonType PersonType { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public DateTime? MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public bool HasPortalAccess { get; set; }
        public PreferredContactMethod? PreferredContactMethod { get; set; }
    }
}
