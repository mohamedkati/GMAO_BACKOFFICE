using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class TechnicianSkillConfiguration : IEntityTypeConfiguration<TechnicianSkill>
    {
        public void Configure(EntityTypeBuilder<TechnicianSkill> builder)
        {
            builder.ToTable("technician_skills");
            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Level).IsRequired().HasConversion<int>();
            builder.Property(ts => ts.CertifiedAt);
            builder.Property(ts => ts.ExpiresAt);

            builder.HasOne(ts => ts.Technician).WithMany(t => t.Skills)
                .HasForeignKey(ts => ts.TechnicianId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ts => ts.Skill).WithMany(s => s.TechnicianSkills)
                .HasForeignKey(ts => ts.SkillId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ts => new { ts.TechnicianId, ts.SkillId }).IsUnique();
        }
    }

}
