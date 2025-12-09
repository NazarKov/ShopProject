using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    public partial class UpdateDicountTableAndUpdateGiftCertificates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "PercentageDiscount",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "GiftCertificates",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "GiftCertificates",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "GiftCertificates",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "create_at",
                table: "GiftCertificates",
                newName: "CreateAt");

            migrationBuilder.AddColumn<int>(
                name: "DiscountID",
                table: "Operation",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TypeDiscount",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InterimAmount",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_DiscountID",
                table: "Operation",
                column: "DiscountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Discount_DiscountID",
                table: "Operation",
                column: "DiscountID",
                principalTable: "Discount",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Discount_DiscountID",
                table: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Operation_DiscountID",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "DiscountID",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "InterimAmount",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "GiftCertificates",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "GiftCertificates",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "GiftCertificates",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "GiftCertificates",
                newName: "create_at");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Operation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "TypeDiscount",
                table: "Discount",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PercentageDiscount",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
