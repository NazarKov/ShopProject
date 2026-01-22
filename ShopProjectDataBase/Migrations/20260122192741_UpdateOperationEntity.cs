using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOperationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfFundsReceived",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "AmountOfIssuedFunds",
                table: "Operation");

            migrationBuilder.AddColumn<string>(
                name: "FiscalServerId",
                table: "Operation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiscalServerId",
                table: "Operation");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfFundsReceived",
                table: "Operation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfIssuedFunds",
                table: "Operation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
