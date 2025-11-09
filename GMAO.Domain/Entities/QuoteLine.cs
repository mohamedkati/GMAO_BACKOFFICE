using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class QuoteLine : BaseEntity<Guid>
    {
        public Guid QuoteId { get; set; }
        public Quote Quote { get; set; } = null!;
        public int LineNumber { get; set; }
        public QuoteItemType Type { get; set; }
        public string? Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Coefficient { get; set; } = 1.30m;
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal LineTotal => Quantity * UnitPrice * (1 - DiscountPercent / 100m);
        public decimal TotalCost => Quantity * CostPrice;
        public decimal Margin => LineTotal - TotalCost;
        public decimal MarginPercent => TotalCost > 0 ? (Margin / TotalCost * 100m) : 0;
        public Guid? GeneratedWorkOrderId { get; set; }
    }
}