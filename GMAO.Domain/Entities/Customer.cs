using GMAO.Domain.Common;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public CustomerType Type { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public PropertyGroup? PropertyGroup { get; set; }
        public PricingCoefficients PricingCoefficients { get; set; } = PricingCoefficients.Default;
        public BillingSettings BillingSettings { get; set; } = BillingSettings.Default;
        public ICollection<Site> Sites { get; set; } = new List<Site>();
        public ICollection<ServiceContract> Contracts { get; set; } = new List<ServiceContract>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<MaintenanceBudget> MaintenanceBudgets { get; set; } = new List<MaintenanceBudget>();
        public string Siren { get; set; } = default!; // SIREN
        public string Comment { get; set; } = default!;

        public Guid CommercialId { get; set; }
        //public Guid? InvoiceAddressId { get; set; }
        //public Guid? MailingAddressId { get; set; }
        public Guid? PaymentMethodId { get; set; } // default payment term ( mode réglement )
        public Address InvoiceAddress { get; set; } = default!;
        public Address MailingAddress { get; set; } = default!;
        public PaymentMethod PaymentMethod { get; set; } = default!;
        public Staff Commercial { get; set; }
        public ICollection<CustomerContact> Contacts { get; set; } = new List<CustomerContact>();
        public ICollection<ServiceRequest> Events { get; set; } = new List<ServiceRequest>();
    }
}
