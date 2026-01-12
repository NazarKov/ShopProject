using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateObjectOwnerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameOwner",
                table: "ObjectOwner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOwner",
                table: "ObjectOwner");
        }
    }
}
