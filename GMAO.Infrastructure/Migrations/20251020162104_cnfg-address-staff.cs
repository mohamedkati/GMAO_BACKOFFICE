using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cnfgaddressstaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_SecondAddressLine",
                table: "Staffs",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "Address_FirstAddressLine",
                table: "Staffs",
                newName: "AddressLine1");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Staffs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Staffs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Staffs",
                newName: "Address_SecondAddressLine");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Staffs",
                newName: "Address_FirstAddressLine");

            migrationBuilder.AlterColumn<string>(
                name: "Address_SecondAddressLine",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_FirstAddressLine",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
