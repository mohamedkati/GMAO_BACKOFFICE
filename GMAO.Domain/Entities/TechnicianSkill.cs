using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class TechnicianSkill : BaseAuditableEntity
    {
        public Guid TechnicianId { get; set; }
        public Technician Technician { get; set; } = null!;
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
        public SkillLevel Level { get; set; }
        public DateTime? CertifiedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}