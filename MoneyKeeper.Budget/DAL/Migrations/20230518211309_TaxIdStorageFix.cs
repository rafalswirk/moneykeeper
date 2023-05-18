using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Budget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TaxIdStorageFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxIdMapping_CategoryMap_CategoryMapId",
                table: "TaxIdMapping");

            migrationBuilder.RenameColumn(
                name: "CategoryMapId",
                table: "TaxIdMapping",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxIdMapping_CategoryMapId",
                table: "TaxIdMapping",
                newName: "IX_TaxIdMapping_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxIdMapping_BudgetCategories_CategoryId",
                table: "TaxIdMapping",
                column: "CategoryId",
                principalTable: "BudgetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxIdMapping_BudgetCategories_CategoryId",
                table: "TaxIdMapping");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TaxIdMapping",
                newName: "CategoryMapId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxIdMapping_CategoryId",
                table: "TaxIdMapping",
                newName: "IX_TaxIdMapping_CategoryMapId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxIdMapping_CategoryMap_CategoryMapId",
                table: "TaxIdMapping",
                column: "CategoryMapId",
                principalTable: "CategoryMap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
