using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixPolicyCardProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DebitCardId",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_DebitCardId",
                table: "Policies",
                column: "DebitCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Cards_DebitCardId",
                table: "Policies",
                column: "DebitCardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Cards_DebitCardId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_DebitCardId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "DebitCardId",
                table: "Policies");
        }
    }
}
