using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class WorkOrderLog : BaseEntity<Guid>
    {
        public Guid WorkOrderId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = default!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public WorkOrder WorkOrder { get; set; } = default!;
    }
}