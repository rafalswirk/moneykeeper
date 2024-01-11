using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoneyKeeper.Transactions.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    OcrDataGenerated = table.Column<bool>(type: "boolean", nullable: false),
                    OcrValidationResult = table.Column<bool>(type: "boolean", nullable: true),
                    SpreadsheetEntered = table.Column<bool>(type: "boolean", nullable: false),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SpreadsheetEnterTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReceiptInfoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_ReceiptInfos_ReceiptInfoId",
                        column: x => x.ReceiptInfoId,
                        principalTable: "ReceiptInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReceiptInfoId",
                table: "Transactions",
                column: "ReceiptInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ReceiptInfos");
        }
    }
}
