namespace GMAO.Application.Features.Customers.DTOs
{
    public class CustomerBudgetDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Year { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal CommittedAmount { get; set; }
        public decimal InvoicedAmount { get; set; }
        public decimal AlertThreshold { get; set; }
        public bool AlertSent { get; set; }
        public decimal RemainingBudget => BudgetedAmount - InvoicedAmount - CommittedAmount;
        public decimal ConsumptionPercent => BudgetedAmount > 0 ? ((InvoicedAmount + CommittedAmount) / BudgetedAmount * 100m) : 0;
    }
}