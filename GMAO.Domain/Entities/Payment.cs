using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class Payment : BaseEntity<Guid>
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public Guid PaymentMethodId { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
    }
}