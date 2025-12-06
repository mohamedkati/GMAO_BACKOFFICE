using GMAO.Domain.Enums;

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
        public int SitesCount { get; set; }
        public int ContactsCount { get; set; }
        public DateTime? lastModified { get; set; }
    }
}