using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateBusinessCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryID",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionID",
                table: "TransactionItems");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItems_TransactionID",
                table: "TransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "BusinessCategories");

            migrationBuilder.AddColumn<int>(
                name: "BusinessTransactionTransactionID",
                table: "TransactionItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessCategories",
                table: "BusinessCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_BusinessTransactionTransactionID",
                table: "TransactionItems",
                column: "BusinessTransactionTransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BusinessCategories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "BusinessCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryID",
                table: "Products",
                column: "SubCategoryID",
                principalTable: "SubCategories",
                principalColumn: "SubItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_BusinessCategories_CategoryID",
                table: "SubCategories",
                column: "CategoryID",
                principalTable: "BusinessCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_BusinessTransactionTransactionID",
                table: "TransactionItems",
                column: "BusinessTransactionTransactionID",
                principalTable: "Transactions",
                principalColumn: "TransactionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BusinessCategories_CategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_BusinessCategories_CategoryID",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_BusinessTransactionTransactionID",
                table: "TransactionItems");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItems_BusinessTransactionTransactionID",
                table: "TransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessCategories",
                table: "BusinessCategories");

            migrationBuilder.DropColumn(
                name: "BusinessTransactionTransactionID",
                table: "TransactionItems");

            migrationBuilder.RenameTable(
                name: "BusinessCategories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionID",
                table: "TransactionItems",
                column: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryID",
                table: "Products",
                column: "SubCategoryID",
                principalTable: "SubCategories",
                principalColumn: "SubItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryID",
                table: "SubCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionID",
                table: "TransactionItems",
                column: "TransactionID",
                principalTable: "Transactions",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
