using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class WorkOrderTask : BaseAuditableEntity
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; } = null!;
        public int TaskOrder { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}