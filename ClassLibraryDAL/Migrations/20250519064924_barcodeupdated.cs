using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class barcodeupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Transactions",
                newName: "TransactionId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackUrl",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeQRCode",
                table: "BusinessDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "BusinessDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "FeedbackUrl",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "IncludeQRCode",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "BusinessDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "BusinessDetails");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Transactions",
                newName: "TransactionID");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionID",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
