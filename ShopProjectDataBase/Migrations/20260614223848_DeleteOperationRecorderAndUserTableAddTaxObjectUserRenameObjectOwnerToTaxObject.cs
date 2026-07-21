using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOperationRecorderAndUserTableAddTaxObjectUserRenameObjectOwnerToTaxObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsRecorder_ObjectOwner_ObjectOwnerID",
                table: "OperationsRecorder");

            migrationBuilder.DropTable(
                name: "ObjectOwner");

            migrationBuilder.DropTable(
                name: "OperationsRecorderAndUser");

            migrationBuilder.RenameColumn(
                name: "ObjectOwnerID",
                table: "OperationsRecorder",
                newName: "TaxObjectID");

            migrationBuilder.RenameIndex(
                name: "IX_OperationsRecorder_ObjectOwnerID",
                table: "OperationsRecorder",
                newName: "IX_OperationsRecorder_TaxObjectID");

            migrationBuilder.CreateTable(
                name: "TaxObject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeObjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameObject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeObject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeStatus = table.Column<int>(type: "int", nullable: false),
                    TypeOfRights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D_ACC_START = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    D_ACC_END = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    C_DISTR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D_LAST_CH = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    C_TERRIT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    REG_NUM_OBJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KATOTTG = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxObject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaxObjectUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxObjectUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaxObjectUser_TaxObject_TaxObjectID",
                        column: x => x.TaxObjectID,
                        principalTable: "TaxObject",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TaxObjectUser_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxObjectUser_TaxObjectID",
                table: "TaxObjectUser",
                column: "TaxObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxObjectUser_UserID",
                table: "TaxObjectUser",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsRecorder_TaxObject_TaxObjectID",
                table: "OperationsRecorder",
                column: "TaxObjectID",
                principalTable: "TaxObject",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsRecorder_TaxObject_TaxObjectID",
                table: "OperationsRecorder");

            migrationBuilder.DropTable(
                name: "TaxObjectUser");

            migrationBuilder.DropTable(
                name: "TaxObject");

            migrationBuilder.RenameColumn(
                name: "TaxObjectID",
                table: "OperationsRecorder",
                newName: "ObjectOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_OperationsRecorder_TaxObjectID",
                table: "OperationsRecorder",
                newName: "IX_OperationsRecorder_ObjectOwnerID");

            migrationBuilder.CreateTable(
                name: "ObjectOwner",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    C_DISTR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    C_TERRIT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeObject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D_ACC_END = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    D_ACC_START = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    D_LAST_CH = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    KATOTTG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameObject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    REG_NUM_OBJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeObjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfRights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectOwner", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OperationsRecorderAndUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpertionsRecordersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsRecorderAndUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OperationsRecorderAndUser_OperationsRecorder_OpertionsRecordersID",
                        column: x => x.OpertionsRecordersID,
                        principalTable: "OperationsRecorder",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OperationsRecorderAndUser_User_UsersID",
                        column: x => x.UsersID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationsRecorderAndUser_OpertionsRecordersID",
                table: "OperationsRecorderAndUser",
                column: "OpertionsRecordersID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsRecorderAndUser_UsersID",
                table: "OperationsRecorderAndUser",
                column: "UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsRecorder_ObjectOwner_ObjectOwnerID",
                table: "OperationsRecorder",
                column: "ObjectOwnerID",
                principalTable: "ObjectOwner",
                principalColumn: "ID");
        }
    }
}
