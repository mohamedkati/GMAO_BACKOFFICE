using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changepermissionmanagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionsId",
                schema: "auth",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                schema: "auth",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "auth",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "auth",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                schema: "auth",
                newName: "RolePermissions");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "RolePermissions",
                newName: "PermissionId");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                schema: "auth",
                table: "TenantUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Action",
                schema: "auth",
                table: "Permissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "auth",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "auth",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDangerous",
                schema: "auth",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                schema: "auth",
                table: "Permissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "auth",
                table: "DomainRoles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                schema: "auth",
                table: "DomainRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "auth",
                table: "DomainRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RolePermissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RolePermissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "RolePermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RolePermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "permission_audits",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    PermissionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Details = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValue: ""),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false, defaultValue: ""),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_permissions",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_permissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "auth",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_permissions_Staffs_UserId",
                        column: x => x.UserId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantUsers_RoleId1",
                schema: "auth",
                table: "TenantUsers",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_DomainRoles_Name",
                schema: "auth",
                table: "DomainRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_permissions_PermissionId",
                schema: "auth",
                table: "user_permissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_user_permissions_UserId_PermissionId",
                schema: "auth",
                table: "user_permissions",
                columns: new[] { "UserId", "PermissionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalSchema: "auth",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_DomainRoles_RoleId1",
                schema: "auth",
                table: "TenantUsers",
                column: "RoleId1",
                principalSchema: "auth",
                principalTable: "DomainRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_DomainRoles_RoleId1",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropTable(
                name: "permission_audits",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "user_permissions",
                schema: "auth");

            migrationBuilder.DropIndex(
                name: "IX_TenantUsers_RoleId1",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropIndex(
                name: "IX_DomainRoles_Name",
                schema: "auth",
                table: "DomainRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropColumn(
                name: "Action",
                schema: "auth",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "auth",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "auth",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsDangerous",
                schema: "auth",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Resource",
                schema: "auth",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "auth",
                table: "DomainRoles");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                schema: "auth",
                table: "DomainRoles");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "auth",
                table: "DomainRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "RolePermissions",
                newSchema: "auth");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                schema: "auth",
                table: "RolePermissions",
                newName: "PermissionsId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "auth",
                table: "Permissions",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionsId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "auth",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionsId",
                schema: "auth",
                table: "RolePermissions",
                column: "PermissionsId",
                principalSchema: "auth",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
