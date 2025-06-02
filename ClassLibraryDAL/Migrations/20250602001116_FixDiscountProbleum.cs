using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class FixDiscountProbleum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_BusinessDetails_BusinessDetailsId",
                table: "Discount");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Transactions_BusinessTransactionId",
                table: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessTransactionId",
                table: "Discount",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessDetailsId",
                table: "Discount",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_BusinessDetails_BusinessDetailsId",
                table: "Discount",
                column: "BusinessDetailsId",
                principalTable: "BusinessDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Transactions_BusinessTransactionId",
                table: "Discount",
                column: "BusinessTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_BusinessDetails_BusinessDetailsId",
                table: "Discount");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Transactions_BusinessTransactionId",
                table: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessTransactionId",
                table: "Discount",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessDetailsId",
                table: "Discount",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_BusinessDetails_BusinessDetailsId",
                table: "Discount",
                column: "BusinessDetailsId",
                principalTable: "BusinessDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Transactions_BusinessTransactionId",
                table: "Discount",
                column: "BusinessTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
