using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class WorkOrderCompletion : BaseAuditableEntity
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; } = null!;
        public DateTime CompletedAt { get; set; }
        public Guid CompletedBy { get; set; }
        public CompletionStatus Status { get; set; }
        public string? WorkPerformed { get; set; }
        public string? Notes { get; set; }
        public Guid? SignatureImageId { get; set; }
        public string? SignedBy { get; set; }
        public DateTime? SignedAt { get; set; }
    }
}