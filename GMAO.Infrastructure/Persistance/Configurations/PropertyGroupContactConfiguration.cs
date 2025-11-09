using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PropertyGroupContactConfiguration : IEntityTypeConfiguration<PropertyGroupContact>
    {
        public void Configure(EntityTypeBuilder<PropertyGroupContact> builder)
        {
            builder.ToTable("property_group_contacts");
            builder.HasKey(c => c.Id);

            // ════════════════════════════════════════════════════════
            // PROPRIÉTÉS
            // ════════════════════════════════════════════════════════

            builder.Property(c => c.Role)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(c => c.PersonType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Position)
                .HasMaxLength(100);

            builder.Property(c => c.Department)
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Phone)
                .HasMaxLength(20);

            builder.Property(c => c.Mobile)
                .HasMaxLength(20);

            builder.Property(c => c.Fax)
                .HasMaxLength(20);

            builder.Property(c => c.IsPrimary)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.ReceivesInvoices)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.ReceivesReports)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.ReceivesAlerts)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.PreferredContactMethod)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(c => c.Notes)
                .HasMaxLength(1000);

            // ════════════════════════════════════════════════════════
            // RELATIONS
            // ════════════════════════════════════════════════════════

            builder.HasOne(c => c.PropertyGroup)
                .WithMany(pg => pg.Contacts)
                .HasForeignKey(c => c.PropertyGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // ════════════════════════════════════════════════════════
            // INDEXES
            // ════════════════════════════════════════════════════════

            builder.HasIndex(c => c.PropertyGroupId)
                .HasDatabaseName("ix_property_group_contacts_property_group_id");

            builder.HasIndex(c => c.Email)
                .HasDatabaseName("ix_property_group_contacts_email");

            builder.HasIndex(c => c.Role)
                .HasDatabaseName("ix_property_group_contacts_role");

            builder.HasIndex(c => new { c.PropertyGroupId, c.IsPrimary })
                .HasDatabaseName("ix_property_group_contacts_group_primary");

            builder.HasIndex(c => c.TenantId)
                .HasDatabaseName("ix_property_group_contacts_tenant_id");

            // ════════════════════════════════════════════════════════
            // IGNORE COMPUTED PROPERTIES
            // ════════════════════════════════════════════════════════

            builder.Ignore(c => c.FullName);
            builder.Ignore(c => c.DomainEvents);
        }
    }
}