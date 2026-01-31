using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteTeamDto
    {
        public SiteStaffDto Commercial { get; set; }
        public SiteStaffDto OperationsManager { get; set; }// responsable d'exploitation
        public SiteStaffDto SectorManager { get; set; }
        public SiteStaffDto Technician1 { get; set; }
        public SiteStaffDto Technician2 { get; set; }
    }

    public record SiteStaffDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Responsibilities { get; set; }
        public string EmployeeNumber { get; set; }
    }
}
