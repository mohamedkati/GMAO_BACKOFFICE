using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class MaintenanceBudget : BaseAuditableEntity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int Year { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal CommittedAmount { get; set; }
        public decimal InvoicedAmount { get; set; }
        public decimal RemainingBudget => BudgetedAmount - InvoicedAmount - CommittedAmount;
        public decimal ConsumptionPercent => BudgetedAmount > 0 ? ((InvoicedAmount + CommittedAmount) / BudgetedAmount * 100m) : 0;
        public decimal AlertThreshold { get; set; } = 80m;
        public bool AlertSent { get; set; }
    }
}