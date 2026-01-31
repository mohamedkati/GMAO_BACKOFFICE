using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Entities;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAO.Domain.Common;

namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteContactDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Note { get; set; }
        public string Fax { get; set; }
        public Guid PersonTypeId { get; set; }
        public Guid SiteContactCategoryId { get; set; }
        public SiteContactTypeDto SiteContactCategory { get; set; }
        public bool IsPrimary { get; set; } = false;
        public string? AvailabilityHours { get; set; }
        public PreferredContactMethod PreferredContactMethod { get; set; }
    }
}
