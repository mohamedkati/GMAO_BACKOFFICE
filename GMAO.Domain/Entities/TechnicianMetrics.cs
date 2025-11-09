using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class TechnicianMetrics : BaseEntity<Guid>
    {
        public Guid TechnicianId { get; set; }
        public Technician Technician { get; set; } = null!;
        public int TotalWorkOrders { get; set; }
        public int CompletedWorkOrders { get; set; }
        public decimal AverageCompletionTime { get; set; }
        public decimal FirstTimeFixRate { get; set; }
        public decimal CustomerSatisfactionScore { get; set; }
    }
}