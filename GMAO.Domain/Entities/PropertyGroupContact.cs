using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class PropertyGroupContact : BaseAuditableEntity
    {
        public Guid PropertyGroupId { get; set; }
        public PropertyGroup PropertyGroup { get; set; } = null!;

        public ContactRole Role { get; set; }
        public PersonType PersonType { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";

        public string? Position { get; set; }
        public string? Department { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }

        public bool IsPrimary { get; set; }
        public bool ReceivesInvoices { get; set; }
        public bool ReceivesReports { get; set; }
        public bool ReceivesAlerts { get; set; }

        public PreferredContactMethod PreferredContactMethod { get; set; }

        public string? Notes { get; set; }
    }
}