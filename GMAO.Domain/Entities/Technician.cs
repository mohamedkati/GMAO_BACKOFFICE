using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class Technician : BaseAuditableEntity
    {
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
        public string? EmployeeNumber { get; set; }
        public TechnicianStatus Status { get; set; }
        public ICollection<WorkOrder> AssignedWorkOrders { get; set; } = new List<WorkOrder>();
        public ICollection<TechnicianSkill> Skills { get; set; } = new List<TechnicianSkill>();
        public TechnicianMetrics? Metrics { get; set; }
    }
}