using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrduName = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionID = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    SubTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Discount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ChangeAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
                    AmountReceived = table.Column<decimal>(type: "TEXT", nullable: false),
                    StaffName = table.Column<string>(type: "TEXT", nullable: true),
                    TableNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Tax = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubItemName = table.Column<string>(type: "TEXT", nullable: false),
                    ParentSubItemID = table.Column<int>(type: "INTEGER", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubItemID);
                    table.ForeignKey(
                        name: "FK_SubCategories_BusinessCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "BusinessCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategories_SubCategories_ParentSubItemID",
                        column: x => x.ParentSubItemID,
                        principalTable: "SubCategories",
                        principalColumn: "SubItemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductUrduName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ProductTitle = table.Column<string>(type: "TEXT", nullable: true),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IsSelected = table.Column<bool>(type: "INTEGER", nullable: false),
                    ActiveDays = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategoryID = table.Column<int>(type: "INTEGER", nullable: true),
                    NestedSubCategoryID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_BusinessCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "BusinessCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategories",
                        principalColumn: "SubItemID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryID",
                table: "Products",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryID",
                table: "SubCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_ParentSubItemID",
                table: "SubCategories",
                column: "ParentSubItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BusinessCategories");
        }
    }
}
