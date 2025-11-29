using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamestafftable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_Staff_CommercialId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Staff_StaffProfilePictureId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_service_requests_Staff_QuoteForId",
                table: "service_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staff_CommercialId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staff_OperationsManagerId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staff_SectorManagerId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staff_Technician1Id",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staff_Technician2Id",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_DomainRoles_RoleId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Users_Id",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_technicians_Staff_StaffId",
                table: "technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_Staff_StaffId",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staffs");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_TenantId",
                table: "Staffs",
                newName: "IX_Staffs_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_Status",
                table: "Staffs",
                newName: "IX_Staffs_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_RoleId",
                table: "Staffs",
                newName: "IX_Staffs_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_EmployeeNumber",
                table: "Staffs",
                newName: "IX_Staffs_EmployeeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_Email",
                table: "Staffs",
                newName: "IX_Staffs_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_Staffs_CommercialId",
                table: "customers",
                column: "CommercialId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Staffs_StaffProfilePictureId",
                table: "Files",
                column: "StaffProfilePictureId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_service_requests_Staffs_QuoteForId",
                table: "service_requests",
                column: "QuoteForId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staffs_CommercialId",
                table: "sites",
                column: "CommercialId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staffs_OperationsManagerId",
                table: "sites",
                column: "OperationsManagerId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staffs_SectorManagerId",
                table: "sites",
                column: "SectorManagerId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staffs_Technician1Id",
                table: "sites",
                column: "Technician1Id",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staffs_Technician2Id",
                table: "sites",
                column: "Technician2Id",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_DomainRoles_RoleId",
                table: "Staffs",
                column: "RoleId",
                principalSchema: "auth",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Users_Id",
                table: "Staffs",
                column: "Id",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_technicians_Staffs_StaffId",
                table: "technicians",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_Staffs_StaffId",
                schema: "auth",
                table: "TenantUsers",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_Staffs_CommercialId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Staffs_StaffProfilePictureId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_service_requests_Staffs_QuoteForId",
                table: "service_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staffs_CommercialId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staffs_OperationsManagerId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staffs_SectorManagerId",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staffs_Technician1Id",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_sites_Staffs_Technician2Id",
                table: "sites");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_DomainRoles_RoleId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Users_Id",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_technicians_Staffs_StaffId",
                table: "technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_Staffs_StaffId",
                schema: "auth",
                table: "TenantUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Staff");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_TenantId",
                table: "Staff",
                newName: "IX_Staff_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_Status",
                table: "Staff",
                newName: "IX_Staff_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_RoleId",
                table: "Staff",
                newName: "IX_Staff_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_EmployeeNumber",
                table: "Staff",
                newName: "IX_Staff_EmployeeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_Email",
                table: "Staff",
                newName: "IX_Staff_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_Staff_CommercialId",
                table: "customers",
                column: "CommercialId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Staff_StaffProfilePictureId",
                table: "Files",
                column: "StaffProfilePictureId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_service_requests_Staff_QuoteForId",
                table: "service_requests",
                column: "QuoteForId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staff_CommercialId",
                table: "sites",
                column: "CommercialId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staff_OperationsManagerId",
                table: "sites",
                column: "OperationsManagerId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staff_SectorManagerId",
                table: "sites",
                column: "SectorManagerId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staff_Technician1Id",
                table: "sites",
                column: "Technician1Id",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_Staff_Technician2Id",
                table: "sites",
                column: "Technician2Id",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_DomainRoles_RoleId",
                table: "Staff",
                column: "RoleId",
                principalSchema: "auth",
                principalTable: "DomainRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Users_Id",
                table: "Staff",
                column: "Id",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_technicians_Staff_StaffId",
                table: "technicians",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_Staff_StaffId",
                schema: "auth",
                table: "TenantUsers",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");
        }
    }
}
