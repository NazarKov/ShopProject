using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGiftCertificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "GiftCertificates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftCertificates_UserID",
                table: "GiftCertificates",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCertificates_User_UserID",
                table: "GiftCertificates",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftCertificates_User_UserID",
                table: "GiftCertificates");

            migrationBuilder.DropIndex(
                name: "IX_GiftCertificates_UserID",
                table: "GiftCertificates");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GiftCertificates");
        }
    }
}
