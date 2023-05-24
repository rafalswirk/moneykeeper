using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Budget.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptInfoUploadDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "UploadDate",
                table: "ReceiptInfos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "ReceiptInfos");
        }
    }
}
