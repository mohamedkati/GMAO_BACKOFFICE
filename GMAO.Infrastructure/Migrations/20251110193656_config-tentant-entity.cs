using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configtentantentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Tenants",
                newName: "Settings_Currency");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Tenants",
                newName: "Address_Country");

            migrationBuilder.RenameIndex(
                name: "IX_property_groups_TenantId",
                table: "property_groups",
                newName: "ix_property_groups_tenant_id");

            migrationBuilder.AlterColumn<string>(
                name: "Settings_Currency",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "Tenants",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Tenants",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_FirstAddressLine",
                table: "Tenants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "Tenants",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_SecondAddressLine",
                table: "Tenants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Tenants",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tenants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tenants",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Features_AdvancedReports",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Features_Analytics",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Features_ApiAccess",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Features_CustomBranding",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Features_MobileApp",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Features_Notifications",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Features_Realtime",
                table: "Tenants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Tenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Tenants",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Tenants",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Settings_DateFormat",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Settings_Language",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Settings_Theme",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Settings_TimeFormat",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Settings_Timezone",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Settings_WeekStartsOn",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subdomain",
                table: "Tenants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Subscription_EndDate",
                table: "Tenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subscription_Plan",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Subscription_StartDate",
                table: "Tenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subscription_Status",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantLimits_Storage",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantLimits_Users",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantLimits_WorkOrders",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Tenants",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "property_groups",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountingContactEmail",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountingContactName",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountingContactPhone",
                table: "property_groups",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "AutoRenewalFrameworkContract",
                table: "property_groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CommercialNotes",
                table: "property_groups",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyRegistrationNumber",
                table: "property_groups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ConsolidatedBilling",
                table: "property_groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FrameworkContractEndDate",
                table: "property_groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrameworkContractReference",
                table: "property_groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FrameworkContractStartDate",
                table: "property_groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternalNotes",
                table: "property_groups",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStatisticsUpdateDate",
                table: "property_groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegalForm",
                table: "property_groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainContactEmail",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainContactMobile",
                table: "property_groups",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainContactName",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainContactPhone",
                table: "property_groups",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainContactPosition",
                table: "property_groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTermsDays",
                table: "property_groups",
                type: "int",
                nullable: false,
                defaultValue: 30);

            migrationBuilder.AddColumn<string>(
                name: "PreferredPaymentMethod",
                table: "property_groups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "property_groups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SIREN",
                table: "property_groups",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "property_groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAnnualRevenue",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalCustomers",
                table: "property_groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSites",
                table: "property_groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalUnits",
                table: "property_groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "property_groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VATNumber",
                table: "property_groups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeDiscountPercent",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_emergency_callout_fee",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_equipment_coefficient",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_labor_coefficient",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_material_coefficient",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "group_minimum_annual_revenue",
                table: "property_groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_monthly_maintenance_fee",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_subcontractor_coefficient",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "group_volume_discount_percent",
                table: "property_groups",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "headquarters_address_line1",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "headquarters_address_line2",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "headquarters_city",
                table: "property_groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "headquarters_country",
                table: "property_groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "headquarters_postal_code",
                table: "property_groups",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "headquarters_street",
                table: "property_groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "property_group_contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    Fax = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ReceivesInvoices = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ReceivesReports = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ReceivesAlerts = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PreferredContactMethod = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, defaultValue: ""),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_property_group_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_property_group_contacts_property_groups_PropertyGroupId",
                        column: x => x.PropertyGroupId,
                        principalTable: "property_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_contract_end_date",
                table: "property_groups",
                column: "FrameworkContractEndDate");

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_main_contact_email",
                table: "property_groups",
                column: "MainContactEmail");

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_name",
                table: "property_groups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_reference",
                table: "property_groups",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_siren",
                table: "property_groups",
                column: "SIREN");

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_status",
                table: "property_groups",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "ix_property_groups_type",
                table: "property_groups",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "ix_property_group_contacts_email",
                table: "property_group_contacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "ix_property_group_contacts_group_primary",
                table: "property_group_contacts",
                columns: new[] { "PropertyGroupId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "ix_property_group_contacts_property_group_id",
                table: "property_group_contacts",
                column: "PropertyGroupId");

            migrationBuilder.CreateIndex(
                name: "ix_property_group_contacts_role",
                table: "property_group_contacts",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "ix_property_group_contacts_tenant_id",
                table: "property_group_contacts",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "property_group_contacts");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_contract_end_date",
                table: "property_groups");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_main_contact_email",
                table: "property_groups");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_name",
                table: "property_groups");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_reference",
                table: "property_groups");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_siren",
                table: "property_groups");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_status",
                table: "property_groups");

            migrationBuilder.DropIndex(
                name: "ix_property_groups_type",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Address_FirstAddressLine",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Address_SecondAddressLine",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_AdvancedReports",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_Analytics",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_ApiAccess",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_CustomBranding",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_MobileApp",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_Notifications",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Features_Realtime",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Settings_DateFormat",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Settings_Language",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Settings_Theme",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Settings_TimeFormat",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Settings_Timezone",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Settings_WeekStartsOn",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Subdomain",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Subscription_EndDate",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Subscription_Plan",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Subscription_StartDate",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Subscription_Status",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "TenantLimits_Storage",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "TenantLimits_Users",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "TenantLimits_WorkOrders",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "AccountingContactEmail",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "AccountingContactName",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "AccountingContactPhone",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "AutoRenewalFrameworkContract",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "CommercialNotes",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "CompanyRegistrationNumber",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "ConsolidatedBilling",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "FrameworkContractEndDate",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "FrameworkContractReference",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "FrameworkContractStartDate",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "InternalNotes",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "LastStatisticsUpdateDate",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "LegalForm",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "LegalName",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "MainContactEmail",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "MainContactMobile",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "MainContactName",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "MainContactPhone",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "MainContactPosition",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "PaymentTermsDays",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "PreferredPaymentMethod",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "SIREN",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "TotalAnnualRevenue",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "TotalCustomers",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "TotalSites",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "TotalUnits",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "VATNumber",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "VolumeDiscountPercent",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_emergency_callout_fee",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_equipment_coefficient",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_labor_coefficient",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_material_coefficient",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_minimum_annual_revenue",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_monthly_maintenance_fee",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_subcontractor_coefficient",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "group_volume_discount_percent",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "headquarters_address_line1",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "headquarters_address_line2",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "headquarters_city",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "headquarters_country",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "headquarters_postal_code",
                table: "property_groups");

            migrationBuilder.DropColumn(
                name: "headquarters_street",
                table: "property_groups");

            migrationBuilder.RenameColumn(
                name: "Settings_Currency",
                table: "Tenants",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Tenants",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "ix_property_groups_tenant_id",
                table: "property_groups",
                newName: "IX_property_groups_TenantId");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Tenants",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "property_groups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
