using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Transactions.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Transactions_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageName = table.Column<string>(type: "TEXT", nullable: false),
                    OcrDataGenerated = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    OcrValidationResult = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    SpreadsheetEntered = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    UploadDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptInfos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptInfos");
        }
    }
}
