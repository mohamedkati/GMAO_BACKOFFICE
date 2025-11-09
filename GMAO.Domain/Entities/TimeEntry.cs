using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class TimeEntry :  BaseAuditableEntity
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; } = null!;
        public Guid TechnicianId { get; set; }
        public Technician Technician { get; set; } = null!;
        public TimeEntryType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public string? Description { get; set; }
        public bool IsBillable { get; set; } = true;
        public decimal? HourlyRate { get; set; }
    }
}