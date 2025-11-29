using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.property_group.queries.ListAllPropertyGroups
{
    public class ListPropertyGroupDto
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PropertyGroupType Type { get; set; }
        public PropertyGroupStatus Status { get; set; }
        public string? LegalName { get; set; }
        public string? SIREN { get; set; } // France: 9 chiffres
        public string? CompanyRegistrationNumber { get; set; } // International
        public LegalForm? LegalForm { get; set; }
        public int Clients { get; set; }
        public int Sites { get; set; }
        public double TotalAnnualRevenue { get; set; } = 1500;
    }
}
