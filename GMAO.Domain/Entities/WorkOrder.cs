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
        public string Reference { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WorkOrderType Type { get; set; }
        public WorkOrderScope Scope { get; set; }
        public PriorityLevel Priority { get; set; }
        public WorkOrderStatus Status { get; set; }
        public WorkOrderVisibility Visibility { get; set; }
        public Guid ServiceRequestId { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
        public Guid? QuoteId { get; set; }
        public Quote? Quote { get; set; }
        public string? BlockingReason { get; set; }
        public DateTime? ExpectedUnblockDate { get; set; }
        public Guid? AssignedTechnicianId { get; set; }
        public Technician? AssignedTechnician { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public UnitAccessRequirement? AccessRequirement { get; set; }
        public WorkOrderCompletion? Completion { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
        public ICollection<UsedPart> UsedParts { get; set; } = new List<UsedPart>();
        public ICollection<WorkOrderTask> Tasks { get; set; } = new List<WorkOrderTask>();
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

        public string? WorkPerformed { get; set; }
        public string? Notes { get; set; }
        public Guid? SignatureImageId { get; set; }
        public string? SignedBy { get; set; }
        public DateTime? SignedAt { get; set; }


        public DateTime? ScheduledAt { get; set; }
        public Guid? AudioFileId { get; set; }
        public Guid? VideoFileId { get; set; }


        public List<File> Files { get; set; }
        public File AudioFile { get; set; }
        public File VideoFile { get; set; }
        public ICollection<WorkOrderLog> Logs { get; set; } = new List<WorkOrderLog>();

        public void AssignTechnician(Guid technicianId)
        { AssignedTechnicianId = technicianId; Status = WorkOrderStatus.Assigned; }

        public void MarkAsCompleted()
        { Status = WorkOrderStatus.Completed; CompletedAt = DateTime.UtcNow; }
    }
}
