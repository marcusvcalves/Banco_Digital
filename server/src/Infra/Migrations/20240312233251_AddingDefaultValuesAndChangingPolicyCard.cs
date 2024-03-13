using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddingDefaultValuesAndChangingPolicyCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Cards_CardId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Cards_DebitCardId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_DebitCardId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "DebitCardId",
                table: "Policies");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Policies",
                newName: "CreditCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_CardId",
                table: "Policies",
                newName: "IX_Policies_CreditCardId");

            migrationBuilder.RenameColumn(
                name: "CardType",
                table: "Cards",
                newName: "cardType");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "Accounts",
                newName: "accountType");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Cards_CreditCardId",
                table: "Policies",
                column: "CreditCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Cards_CreditCardId",
                table: "Policies");

            migrationBuilder.RenameColumn(
                name: "CreditCardId",
                table: "Policies",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_CreditCardId",
                table: "Policies",
                newName: "IX_Policies_CardId");

            migrationBuilder.RenameColumn(
                name: "cardType",
                table: "Cards",
                newName: "CardType");

            migrationBuilder.RenameColumn(
                name: "accountType",
                table: "Accounts",
                newName: "AccountType");

            migrationBuilder.AddColumn<int>(
                name: "DebitCardId",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_DebitCardId",
                table: "Policies",
                column: "DebitCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Cards_CardId",
                table: "Policies",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Cards_DebitCardId",
                table: "Policies",
                column: "DebitCardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }
    }
}
