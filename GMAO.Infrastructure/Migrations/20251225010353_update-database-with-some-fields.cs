using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabasewithsomefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sites_sector_types_MarketTypeId",
                table: "sites");

            migrationBuilder.RenameColumn(
                name: "MarketTypeId",
                table: "sites",
                newName: "SectorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_sites_MarketTypeId",
                table: "sites",
                newName: "IX_sites_SectorTypeId");

            migrationBuilder.AddColumn<int>(
                name: "SurfaceArea",
                table: "sites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsibilities",
                schema: "auth",
                table: "DomainRoles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_sector_types_SectorTypeId",
                table: "sites",
                column: "SectorTypeId",
                principalTable: "sector_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sites_sector_types_SectorTypeId",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "SurfaceArea",
                table: "sites");

            migrationBuilder.DropColumn(
                name: "Responsibilities",
                schema: "auth",
                table: "DomainRoles");

            migrationBuilder.RenameColumn(
                name: "SectorTypeId",
                table: "sites",
                newName: "MarketTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_sites_SectorTypeId",
                table: "sites",
                newName: "IX_sites_MarketTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_sites_sector_types_MarketTypeId",
                table: "sites",
                column: "MarketTypeId",
                principalTable: "sector_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
