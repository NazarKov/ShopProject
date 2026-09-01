using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaxObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LoadTaxServer",
                table: "TaxObject",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoadTaxServer",
                table: "TaxObject");
        }
    }
}
