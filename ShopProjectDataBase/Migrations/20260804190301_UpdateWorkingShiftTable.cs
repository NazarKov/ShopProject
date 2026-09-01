using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkingShiftTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfOfficialFundsIssuedCard",
                table: "WorkingShift");

            migrationBuilder.DropColumn(
                name: "AmountOfOfficialFundsReceivedCard",
                table: "WorkingShift");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfOfficialFundsIssuedCard",
                table: "WorkingShift",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfOfficialFundsReceivedCard",
                table: "WorkingShift",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
