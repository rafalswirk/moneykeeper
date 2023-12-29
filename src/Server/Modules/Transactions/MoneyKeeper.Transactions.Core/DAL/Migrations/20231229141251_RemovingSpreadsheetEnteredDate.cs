using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Transactions.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovingSpreadsheetEnteredDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpreadheetEntryDate",
                table: "ReceiptInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SpreadheetEntryDate",
                table: "ReceiptInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
