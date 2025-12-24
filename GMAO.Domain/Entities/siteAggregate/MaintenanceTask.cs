using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class MaintenanceTask : BaseEntity<Guid>
    {
        public Guid MaintenancePlanId { get; set; }
        public MaintenancePlan MaintenancePlan { get; set; } = null!;
        public int TaskOrder { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? EstimatedDurationMinutes { get; set; }
        public Guid? RequiredSkillId { get; set; }
        public Skill? RequiredSkill { get; set; }
    }
}