using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class MaintenancePlan : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
        public MaintenanceFrequency Frequency { get; set; }
        public DateTime? LastExecutionDate { get; set; }
        public DateTime? NextExecutionDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int AlertDaysBefore { get; set; } = 7;
        public ICollection<MaintenanceTask> Tasks { get; set; } = new List<MaintenanceTask>();
    }
}