using GMAO.Domain.Entities.siteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class OccupantConfiguration : IEntityTypeConfiguration<Occupant>
    {
        public void Configure(EntityTypeBuilder<Occupant> builder)
        {
            builder.ToTable("occupants");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Type).IsRequired().HasConversion<int>();
            builder.Property(o => o.PersonType).IsRequired().HasConversion<int>();
            builder.Property(o => o.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(o => o.LastName).IsRequired().HasMaxLength(100);
            builder.Property(o => o.CompanyName).HasMaxLength(200);
            builder.Property(o => o.Email).HasMaxLength(200);
            builder.Property(o => o.Phone).HasMaxLength(20);
            builder.Property(o => o.Mobile).HasMaxLength(20);
            builder.Property(o => o.MoveInDate);
            builder.Property(o => o.MoveOutDate);
            builder.Property(o => o.HasPortalAccess).IsRequired();
            builder.Property(o => o.PreferredContactMethod).HasConversion<int>();

            builder.HasOne(o => o.Unit).WithMany(u => u.Occupants)
                .HasForeignKey(o => o.UnitId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ServiceRequests)
                .WithOne(x => x.Occupant)
                .HasForeignKey(x => x.OccupantId)
                .IsRequired(false);

            builder.HasIndex(o => o.UnitId);
            builder.HasIndex(o => o.Email);
            builder.HasIndex(o => o.Type);
            builder.HasIndex(o => o.TenantId);
            builder.Ignore(o => o.DomainEvents);
        }
    }


}
