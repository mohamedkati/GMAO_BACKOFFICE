using GMAO.Application.SharedBusiness.Dtos.payment_method;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.DTOs
{
    public class CustomerDetailedDto
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public CustomerType Type { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public PricingCoefficients PricingCoefficients { get; set; } = PricingCoefficients.Default;
        public BillingSettings BillingSettings { get; set; } = default!;
        public string Siren { get; set; } = default!;
        public string Comment { get; set; } = default!;
        public Guid CommercialId { get; set; }
        public Address InvoiceAddress { get; set; } = default!;
        public Address MailingAddress { get; set; } = default!;
        public Guid? PaymentMethodId { get; set; }
        public IReadOnlyList<CustomerContactDto> Contacts { get; set; } = new List<CustomerContactDto>();
        public IReadOnlyList<CustomerBudgetDto> MaintenanceBudgets { get; set; } = new List<CustomerBudgetDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public PaymentMethodAsKeyValueDto PaymentMethod { get; set; }
    }
}
