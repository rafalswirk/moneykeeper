using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoneyKeeper.Budget.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SpreadsheetModificationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModificationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SheetName = table.Column<string>(type: "text", nullable: false),
                    Row = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Column = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationHistory");
        }
    }
}
