using GMAO.Domain.Common;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class WorkOrder : BaseAuditableEntity
    {
        public WorkOrderType Type { get; set; }
        public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Requested;
        public string Description { get; set; } = default!;
        public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

        public Guid EventId { get; set; }
        public Guid? QuoteId { get; set; }
        public Guid? AssignedTechnicianId { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Guid? AudioFileId { get; set; }
        public Guid? VideoFileId { get; set; }

        
        public Event Event { get; set; }
        public Quote? Quote { get; set; }
        public Staff? AssignedTechnician { get; set; }
        public  List<File> Files { get; set; }
        public  File AudioFile { get; set; }
        public File VideoFile { get; set; }
        public ICollection<WorkOrderLog> Logs { get; set; } = new List<WorkOrderLog>();

        public void AssignTechnician(Guid technicianId)
        { AssignedTechnicianId = technicianId; Status = WorkOrderStatus.Assigned; }

        public void MarkAsCompleted()
        { Status = WorkOrderStatus.Completed; CompletedAt = DateTime.UtcNow; }
    }
}
