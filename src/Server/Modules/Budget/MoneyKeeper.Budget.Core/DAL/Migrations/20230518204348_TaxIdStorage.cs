using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoneyKeeper.Budget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TaxIdStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "TaxIdMapping");

            migrationBuilder.AddColumn<int>(
                name: "TaxIdId",
                table: "TaxIdMapping",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaxIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxIdentificationNumber = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxIds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxIdMapping_TaxIdId",
                table: "TaxIdMapping",
                column: "TaxIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxIdMapping_TaxIds_TaxIdId",
                table: "TaxIdMapping",
                column: "TaxIdId",
                principalTable: "TaxIds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxIdMapping_TaxIds_TaxIdId",
                table: "TaxIdMapping");

            migrationBuilder.DropTable(
                name: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_TaxIdMapping_TaxIdId",
                table: "TaxIdMapping");

            migrationBuilder.DropColumn(
                name: "TaxIdId",
                table: "TaxIdMapping");

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "TaxIdMapping",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
