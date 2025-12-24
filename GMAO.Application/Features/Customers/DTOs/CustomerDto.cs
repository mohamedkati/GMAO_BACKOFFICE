using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;

namespace GMAO.Application.Features.Customers.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public CustomerType Type { get; set; }
        public string PropertyGroupName { get; set; } = string.Empty;
        public string CommercialName { get; set; } = string.Empty;
        public string InvoiceCity { get; set; } = string.Empty;
        public int SitesCount { get; set; }
        public int ContactsCount { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Contact principal (juste email/phone)
        public string? PrimaryContactEmail { get; set; }
        public string? PrimaryContactPhone { get; set; }
        public string? PrimaryContactName { get; set; }

        // Budget total (calculé)
        public decimal TotalBudget { get; set; }
    }
}