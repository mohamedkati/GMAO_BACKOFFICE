using GMAO.Domain.Common;
using GMAO.Domain.Entities.siteAggregate;

namespace GMAO.Domain.Entities
{
    public class Skill : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public ICollection<TechnicianSkill> TechnicianSkills { get; set; } = new List<TechnicianSkill>();
        public ICollection<MaintenanceTask> MaintenanceTaskSkills { get; set; } = new List<MaintenanceTask>();
    }
}