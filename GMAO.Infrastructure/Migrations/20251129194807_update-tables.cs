using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMAO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_PaymentMethod_PaymentMethodId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_PaymentMethod_PaymentMethodId",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "BillingSettings_PaymentTermsDays",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "PaymentMethod",
                newName: "payment_methods");

            migrationBuilder.AlterColumn<string>(
                name: "Terms",
                table: "payment_methods",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "payment_methods",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DueDays",
                table: "payment_methods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_methods",
                table: "payment_methods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_payment_methods_PaymentMethodId",
                table: "customers",
                column: "PaymentMethodId",
                principalTable: "payment_methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_payment_methods_PaymentMethodId",
                table: "payments",
                column: "PaymentMethodId",
                principalTable: "payment_methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_payment_methods_PaymentMethodId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_payment_methods_PaymentMethodId",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_methods",
                table: "payment_methods");

            migrationBuilder.DropColumn(
                name: "DueDays",
                table: "payment_methods");

            migrationBuilder.RenameTable(
                name: "payment_methods",
                newName: "PaymentMethod");

            migrationBuilder.AddColumn<int>(
                name: "BillingSettings_PaymentTermsDays",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Terms",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_PaymentMethod_PaymentMethodId",
                table: "customers",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_PaymentMethod_PaymentMethodId",
                table: "payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
