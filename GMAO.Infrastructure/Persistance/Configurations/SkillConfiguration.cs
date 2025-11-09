using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("skills");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Description).HasMaxLength(500);
            builder.Property(s => s.Category).HasMaxLength(100);

            builder.HasMany(s => s.TechnicianSkills).WithOne(ts => ts.Skill)
                .HasForeignKey(ts => ts.SkillId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.Name).IsUnique();
            builder.HasIndex(s => s.Category);
            builder.Ignore(s => s.DomainEvents);
        }
    }

}
