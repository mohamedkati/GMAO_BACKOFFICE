using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatesiteaggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sites_MarketType_MarketTypeId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_SiteClientType_SiteClientTypeId",
                table: "sites");

            migrationBuilder.DropTable(
                name: "MarketType");

            migrationBuilder.DropTable(
                name: "SiteClientType");

            migrationBuilder.DropTable(
                name: "SiteCategory");

            migrationBuilder.RenameColumn(
                name: "SiteClientTypeId",
                table: "sites",
                newName: "PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_sites_SiteClientTypeId",
                table: "sites",
                newName: "IX_sites_PaymentMethodId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "sites",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "sites",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingCountry",
                table: "sites",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingLigne1",
                table: "sites",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingLigne2",
                table: "sites",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingPostalCode",
                table: "sites",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingStreet",
                table: "sites",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientTypeId",
                table: "sites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CommentReport",
                table: "sites",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceMailAddress",
                table: "sites",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainMailAddress",
                table: "sites",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Siren",
                table: "sites",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Siret",
                table: "sites",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_AccessCodes",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_AccessRestrictions",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_GeneralInstructions",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_KeyInstructions",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_ParkingInfo",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SiteAccessInfo_RequiresBadge",
                table: "sites",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_SafetyRequirements",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAccessInfo_WorkingHours",
                table: "sites",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "VatId",
                table: "sites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Location_LocationDescription",
                table: "assets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Location_PlanDocumentId",
                table: "assets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Location_XPosition",
                table: "assets",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Location_YPosition",
                table: "assets",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sector_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
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
                    table.PrimaryKey("PK_sector_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "site_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, defaultValue: ""),
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
                    table.PrimaryKey("PK_site_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "site_documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false, defaultValue: ""),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValue: ""),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, defaultValue: ""),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SendExpirationAlert = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsPlan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_site_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_site_documents_sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_keepers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    CellPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_site_keepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_site_keepers_sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, defaultValue: ""),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    ValuRate = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "site_contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    CellPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    PersonTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteContactCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AvailabilityHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    PreferredContactMethod = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_site_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_site_contacts_ContactType_SiteContactCategoryId",
                        column: x => x.SiteContactCategoryId,
                        principalTable: "ContactType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_site_contacts_sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_client_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    SiteCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_site_client_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_site_client_types_site_categories_SiteCategoryId",
                        column: x => x.SiteCategoryId,
                        principalTable: "site_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sites_ClientTypeId",
                table: "sites",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_sites_VatId",
                table: "sites",
                column: "VatId");

            migrationBuilder.CreateIndex(
                name: "IX_assets_Location_PlanDocumentId",
                table: "assets",
                column: "Location_PlanDocumentId",
                unique: true,
                filter: "[Location_PlanDocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_asset_categories_Name",
                table: "asset_categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_site_client_types_SiteCategoryId",
                table: "site_client_types",
                column: "SiteCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_site_contacts_Email",
                table: "site_contacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_site_contacts_Firstname_Lastname",
                table: "site_contacts",
                columns: new[] { "Firstname", "Lastname" });

            migrationBuilder.CreateIndex(
                name: "IX_site_contacts_SiteContactCategoryId",
                table: "site_contacts",
                column: "SiteContactCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_site_contacts_SiteId",
                table: "site_contacts",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_site_contacts_TenantId",
                table: "site_contacts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_site_documents_SiteId",
                table: "site_documents",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_site_keepers_SiteId",
                table: "site_keepers",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_assets_site_documents_Location_PlanDocumentId",
                table: "assets",
                column: "Location_PlanDocumentId",
                principalTable: "site_documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sites_payment_methods_PaymentMethodId",
                table: "sites",
                column: "PaymentMethodId",
                principalTable: "payment_methods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_sector_types_MarketTypeId",
                table: "sites",
                column: "MarketTypeId",
                principalTable: "sector_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sites_site_client_types_ClientTypeId",
                table: "sites",
                column: "ClientTypeId",
                principalTable: "site_client_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sites_vats_VatId",
                table: "sites",
                column: "VatId",
                principalTable: "vats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assets_site_documents_Location_PlanDocumentId",
                table: "assets");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_payment_methods_PaymentMethodId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_sector_types_MarketTypeId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_site_client_types_ClientTypeId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_vats_VatId",
                table: "sites");

            migrationBuilder.DropTable(
                name: "sector_types");

            migrationBuilder.DropTable(
                name: "site_client_types");

            migrationBuilder.DropTable(
                name: "site_contacts");

            migrationBuilder.DropTable(
                name: "site_documents");

            migrationBuilder.DropTable(
                name: "site_keepers");

            migrationBuilder.DropTable(
                name: "vats");

            migrationBuilder.DropTable(
                name: "site_categories");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropIndex(
                name: "IX_sites_ClientTypeId",
                table: "sites");

            migrationBuilder.DropIndex(
                name: "IX_sites_VatId",
                table: "sites");

            migrationBuilder.DropIndex(
                name: "IX_assets_Location_PlanDocumentId",
                table: "assets");

            migrationBuilder.DropIndex(
                name: "IX_asset_categories_Name",
                table: "asset_categories");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "BillingCountry",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "BillingLigne1",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "BillingLigne2",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "BillingPostalCode",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "BillingStreet",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "ClientTypeId",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "CommentReport",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "InvoiceMailAddress",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "MainMailAddress",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "Siren",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "Siret",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_AccessCodes",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_AccessRestrictions",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_GeneralInstructions",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_KeyInstructions",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_ParkingInfo",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_RequiresBadge",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_SafetyRequirements",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SiteAccessInfo_WorkingHours",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "VatId",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "Location_LocationDescription",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "Location_PlanDocumentId",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "Location_XPosition",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "Location_YPosition",
                table: "assets");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "sites",
                newName: "SiteClientTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_sites_PaymentMethodId",
                table: "sites",
                newName: "IX_sites_SiteClientTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "sites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.CreateTable(
                name: "MarketType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteClientType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteClientType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteClientType_SiteCategory_SiteCategoryId",
                        column: x => x.SiteCategoryId,
                        principalTable: "SiteCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteClientType_SiteCategoryId",
                table: "SiteClientType",
                column: "SiteCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_MarketType_MarketTypeId",
                table: "sites",
                column: "MarketTypeId",
                principalTable: "MarketType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sites_SiteClientType_SiteClientTypeId",
                table: "sites",
                column: "SiteClientTypeId",
                principalTable: "SiteClientType",
                principalColumn: "Id");
        }
    }
}
