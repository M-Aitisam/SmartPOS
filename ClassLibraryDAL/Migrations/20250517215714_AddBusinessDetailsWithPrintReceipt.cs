using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessDetailsWithPrintReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DefaultTaxRate",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: 15.0m);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeBarcode",
                table: "BusinessDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptFooter",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiptHeader",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxNumber",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultTaxRate",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "IncludeBarcode",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "ReceiptFooter",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "ReceiptHeader",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "TaxNumber",
                table: "BusinessDetails");
        }
    }
}
