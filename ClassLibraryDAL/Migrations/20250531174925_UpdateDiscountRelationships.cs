using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Transactions");

            migrationBuilder.AddColumn<bool>(
                name: "EnableDiscounts",
                table: "BusinessDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsPercentage = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    BusinessModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessDetailsId = table.Column<int>(type: "INTEGER", nullable: true),
                    BusinessTransactionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discount_BusinessDetails_BusinessDetailsId",
                        column: x => x.BusinessDetailsId,
                        principalTable: "BusinessDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discount_Businesses_BusinessModelId",
                        column: x => x.BusinessModelId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discount_Transactions_BusinessTransactionId",
                        column: x => x.BusinessTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BusinessDetailsId",
                table: "Discount",
                column: "BusinessDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BusinessModelId",
                table: "Discount",
                column: "BusinessModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BusinessTransactionId",
                table: "Discount",
                column: "BusinessTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropColumn(
                name: "EnableDiscounts",
                table: "BusinessDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
