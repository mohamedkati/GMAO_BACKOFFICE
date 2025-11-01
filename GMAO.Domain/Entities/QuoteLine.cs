using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class QuoteLine : BaseEntity<Guid>
    {
        public Guid QuoteId { get; set; }
        public string Description { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}