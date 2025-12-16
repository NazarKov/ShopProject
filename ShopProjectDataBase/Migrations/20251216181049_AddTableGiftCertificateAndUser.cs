using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddTableGiftCertificateAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "GiftCertificates");

            migrationBuilder.CreateTable(
                name: "GiftCertificateAndUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiftCertificatesID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCertificateAndUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GiftCertificateAndUsers_GiftCertificates_GiftCertificatesID",
                        column: x => x.GiftCertificatesID,
                        principalTable: "GiftCertificates",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_GiftCertificateAndUsers_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiftCertificateAndUsers_GiftCertificatesID",
                table: "GiftCertificateAndUsers",
                column: "GiftCertificatesID");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCertificateAndUsers_UserID",
                table: "GiftCertificateAndUsers",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftCertificateAndUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "GiftCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
