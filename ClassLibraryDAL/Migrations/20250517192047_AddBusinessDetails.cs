using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "LogoData",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ParentName",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "HasWiFi",
                table: "Businesses",
                newName: "GeneralInformationId");

            migrationBuilder.RenameColumn(
                name: "HasOutdoorSeating",
                table: "Businesses",
                newName: "BusinessDetailsId");

            migrationBuilder.CreateTable(
                name: "BusinessDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusinessName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LogoPath = table.Column<string>(type: "TEXT", nullable: false),
                    LogoData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    BusinessType = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Area = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    HasWiFi = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasOutdoorSeating = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    ParentName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PinCode = table.Column<string>(type: "TEXT", nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralInformation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessDetailsId",
                table: "Businesses",
                column: "BusinessDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_GeneralInformationId",
                table: "Businesses",
                column: "GeneralInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessDetails_BusinessDetailsId",
                table: "Businesses",
                column: "BusinessDetailsId",
                principalTable: "BusinessDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_GeneralInformation_GeneralInformationId",
                table: "Businesses",
                column: "GeneralInformationId",
                principalTable: "GeneralInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessDetails_BusinessDetailsId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_GeneralInformation_GeneralInformationId",
                table: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinessDetails");

            migrationBuilder.DropTable(
                name: "GeneralInformation");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessDetailsId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_GeneralInformationId",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "GeneralInformationId",
                table: "Businesses",
                newName: "HasWiFi");

            migrationBuilder.RenameColumn(
                name: "BusinessDetailsId",
                table: "Businesses",
                newName: "HasOutdoorSeating");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Businesses",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessType",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Businesses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "LogoData",
                table: "Businesses",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Businesses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentName",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PinCode",
                table: "Businesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "Businesses",
                type: "TEXT",
                nullable: true);
        }
    }
}
