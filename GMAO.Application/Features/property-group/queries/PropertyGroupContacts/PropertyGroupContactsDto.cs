using GMAO.Domain.Enums;

namespace GMAO.Application.Features.property_group.queries.PropertyGroupContacts
{
    public class PropertyGroupContactsDto
    {
        public Guid Id { get; set; }
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