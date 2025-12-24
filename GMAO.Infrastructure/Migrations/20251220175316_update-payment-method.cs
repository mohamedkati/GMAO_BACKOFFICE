using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatepaymentmethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_DomainRoles_RoleId1",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropIndex(
                name: "IX_TenantUsers_RoleId1",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "payment_methods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeDueDate",
                table: "payment_methods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "payment_methods");

            migrationBuilder.DropColumn(
                name: "TypeDueDate",
                table: "payment_methods");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                schema: "auth",
                table: "TenantUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantUsers_RoleId1",
                schema: "auth",
                table: "TenantUsers",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_DomainRoles_RoleId1",
                schema: "auth",
                table: "TenantUsers",
                column: "RoleId1",
                principalSchema: "auth",
                principalTable: "DomainRoles",
                principalColumn: "Id");
        }
    }
}
