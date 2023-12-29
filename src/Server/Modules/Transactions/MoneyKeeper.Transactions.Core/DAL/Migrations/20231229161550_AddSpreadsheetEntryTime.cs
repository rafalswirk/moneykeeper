using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Transactions.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSpreadsheetEntryTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SpreadsheetEnterTime",
                table: "ReceiptInfos",
                type: "TimestampTz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpreadsheetEnterTime",
                table: "ReceiptInfos");
        }
    }
}
