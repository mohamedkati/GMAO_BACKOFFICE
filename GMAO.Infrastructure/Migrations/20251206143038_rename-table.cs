using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renametable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_DomainRoles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "role_permissions",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                schema: "auth",
                table: "role_permissions",
                newName: "IX_role_permissions_RoleId_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "auth",
                table: "role_permissions",
                newName: "IX_role_permissions_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                schema: "auth",
                table: "role_permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_DomainRoles_RoleId",
                schema: "auth",
                table: "role_permissions",
                column: "RoleId",
                principalSchema: "auth",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_Permissions_PermissionId",
                schema: "auth",
                table: "role_permissions",
                column: "PermissionId",
                principalSchema: "auth",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_DomainRoles_RoleId",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_Permissions_PermissionId",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.RenameTable(
                name: "role_permissions",
                schema: "auth",
                newName: "RolePermissions");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_RoleId_PermissionId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RoleId_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_PermissionId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_DomainRoles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalSchema: "auth",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalSchema: "auth",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
