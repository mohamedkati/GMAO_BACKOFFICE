using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class PropertyGroupConfiguration : IEntityTypeConfiguration<PropertyGroup>
    {
        public void Configure(EntityTypeBuilder<PropertyGroup> builder)
        {
            builder.ToTable("property_groups");
            builder.HasKey(pg => pg.Id);

            // ════════════════════════════════════════════════════════
            // INFORMATIONS DE BASE
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.Reference)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pg => pg.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pg => pg.Description)
                .HasMaxLength(1000);

            builder.Property(pg => pg.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(pg => pg.Status)
                .IsRequired()
                .HasConversion<int>();

            // ════════════════════════════════════════════════════════
            // INFORMATIONS LÉGALES
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.LegalName)
                .HasMaxLength(200);

            builder.Property(pg => pg.SIREN)
                .HasMaxLength(9)
                .IsFixedLength(false);

            builder.Property(pg => pg.CompanyRegistrationNumber)
                .HasMaxLength(50);

            builder.Property(pg => pg.VATNumber)
                .HasMaxLength(50);

            builder.Property(pg => pg.LegalForm)
                .HasConversion<int>();

            // ════════════════════════════════════════════════════════
            // ADRESSE SIÈGE SOCIAL (Value Object)
            // ════════════════════════════════════════════════════════

            builder.OwnsOne(pg => pg.HeadquartersAddress, address =>
            {
                address.Property(a => a.FirstAddressLine)
                    .HasColumnName("headquarters_address_line1")
                    .HasMaxLength(200);

                address.Property(a => a.SecondAddressLine)
                    .HasColumnName("headquarters_address_line2")
                    .HasMaxLength(200);

                address.Property(a => a.City)
                    .HasColumnName("headquarters_city")
                    .HasMaxLength(100);

                address.Property(a => a.Street)
                    .HasColumnName("headquarters_street")
                    .HasMaxLength(200);

                address.Property(a => a.PostalCode)
                    .HasColumnName("headquarters_postal_code")
                    .HasMaxLength(20);

                address.Property(a => a.Country)
                    .HasColumnName("headquarters_country")
                    .HasMaxLength(100);
            });

            // ════════════════════════════════════════════════════════
            // CONTACT PRINCIPAL
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.MainContactName)
                .HasMaxLength(200);

            builder.Property(pg => pg.MainContactPosition)
                .HasMaxLength(100);

            builder.Property(pg => pg.MainContactEmail)
                .HasMaxLength(200);

            builder.Property(pg => pg.MainContactPhone)
                .HasMaxLength(20);

            builder.Property(pg => pg.MainContactMobile)
                .HasMaxLength(20);

            // ════════════════════════════════════════════════════════
            // CONTACT COMPTABILITÉ
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.AccountingContactName)
                .HasMaxLength(200);

            builder.Property(pg => pg.AccountingContactEmail)
                .HasMaxLength(200);

            builder.Property(pg => pg.AccountingContactPhone)
                .HasMaxLength(20);

            // ════════════════════════════════════════════════════════
            // PARAMÈTRES FACTURATION GROUPE
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.ConsolidatedBilling)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(pg => pg.PaymentTermsDays)
                .IsRequired()
                .HasDefaultValue(30);

            builder.Property(pg => pg.VolumeDiscountPercent)
                .HasPrecision(5, 2);

            builder.Property(pg => pg.PreferredPaymentMethod)
                .HasMaxLength(50);

            // ════════════════════════════════════════════════════════
            // TARIFICATION GROUPE (Value Object)
            // ════════════════════════════════════════════════════════

            builder.OwnsOne(pg => pg.GroupPricingCoefficients, pricing =>
            {
                pricing.Property(p => p.LaborCoefficient)
                    .HasColumnName("group_labor_coefficient")
                    .HasPrecision(5, 2)
                    .IsRequired();

                pricing.Property(p => p.MaterialCoefficient)
                    .HasColumnName("group_material_coefficient")
                    .HasPrecision(5, 2)
                    .IsRequired();

                pricing.Property(p => p.EquipmentCoefficient)
                    .HasColumnName("group_equipment_coefficient")
                    .HasPrecision(5, 2)
                    .IsRequired();

                pricing.Property(p => p.SubcontractorCoefficient)
                    .HasColumnName("group_subcontractor_coefficient")
                    .HasPrecision(5, 2)
                    .IsRequired();

                pricing.Property(p => p.VolumeDiscountPercent)
                    .HasColumnName("group_volume_discount_percent")
                    .HasPrecision(5, 2)
                    .IsRequired();

                pricing.Property(p => p.MinimumAnnualRevenue)
                    .HasColumnName("group_minimum_annual_revenue")
                    .IsRequired();

                pricing.Property(p => p.EmergencyCalloutFee)
                    .HasColumnName("group_emergency_callout_fee")
                    .HasPrecision(10, 2);

                pricing.Property(p => p.MonthlyMaintenanceFee)
                    .HasColumnName("group_monthly_maintenance_fee")
                    .HasPrecision(10, 2);
            });

            // ════════════════════════════════════════════════════════
            // STATISTIQUES
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.TotalCustomers)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(pg => pg.TotalSites)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(pg => pg.TotalUnits)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(pg => pg.TotalAnnualRevenue)
                .HasPrecision(18, 2)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(pg => pg.LastStatisticsUpdateDate);

            // ════════════════════════════════════════════════════════
            // CONTRAT CADRE
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.FrameworkContractStartDate);

            builder.Property(pg => pg.FrameworkContractEndDate);

            builder.Property(pg => pg.FrameworkContractReference)
                .HasMaxLength(100);

            builder.Property(pg => pg.AutoRenewalFrameworkContract)
                .IsRequired()
                .HasDefaultValue(false);

            // ════════════════════════════════════════════════════════
            // NOTES
            // ════════════════════════════════════════════════════════

            builder.Property(pg => pg.InternalNotes)
                .HasMaxLength(2000);

            builder.Property(pg => pg.CommercialNotes)
                .HasMaxLength(2000);

            // ════════════════════════════════════════════════════════
            // RELATIONS
            // ════════════════════════════════════════════════════════

            builder.HasMany(pg => pg.Customers)
                .WithOne(c => c.PropertyGroup)
                .HasForeignKey(c => c.PropertyGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(pg => pg.Contacts)
                .WithOne(c => c.PropertyGroup)
                .HasForeignKey(c => c.PropertyGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // ════════════════════════════════════════════════════════
            // INDEXES
            // ════════════════════════════════════════════════════════

            builder.HasIndex(pg => pg.Reference)
                .IsUnique()
                .HasDatabaseName("ix_property_groups_reference");

            builder.HasIndex(pg => pg.Name)
                .HasDatabaseName("ix_property_groups_name");

            builder.HasIndex(pg => pg.SIREN)
                .HasDatabaseName("ix_property_groups_siren");

            builder.HasIndex(pg => pg.Type)
                .HasDatabaseName("ix_property_groups_type");

            builder.HasIndex(pg => pg.Status)
                .HasDatabaseName("ix_property_groups_status");

            builder.HasIndex(pg => pg.MainContactEmail)
                .HasDatabaseName("ix_property_groups_main_contact_email");

            builder.HasIndex(pg => pg.FrameworkContractEndDate)
                .HasDatabaseName("ix_property_groups_contract_end_date");

            builder.HasIndex(pg => pg.TenantId)
                .HasDatabaseName("ix_property_groups_tenant_id");

            // ════════════════════════════════════════════════════════
            // IGNORE COMPUTED PROPERTIES
            // ════════════════════════════════════════════════════════

            builder.Ignore(pg => pg.DomainEvents);

        }
    }
}