using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using MediatR;

namespace GMAO.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<ResponseResult<bool>>
    {
        public string Reference { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public CustomerType Type { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public PricingCoefficients PricingCoefficients { get; set; } = PricingCoefficients.Default;
        public BillingSettings BillingSettings { get; set; } = BillingSettings.Default;
        public string Siren { get; set; } = default!; // SIREN
        public string Comment { get; set; } = default!;

        public Guid CommercialId { get; set; }
        public Guid? PaymentMethodId { get; set; } // default payment term ( mode réglement )
        public Address InvoiceAddress { get; set; } = default!;
        public Address MailingAddress { get; set; } = default!;
    }
}
