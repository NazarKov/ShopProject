using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDataBase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeUKTZED",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeUKTZED", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameDiscount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentageDiscount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GiftCertificates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCertificates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectOwner",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ObjectOwner", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortNameUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeAccess = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserSignatureKey",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Signature = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SignaturePassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignatureKey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OperationsRecorder",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiscalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeStatus = table.Column<int>(type: "int", nullable: false),
                    D_REG = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsRecorder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OperationsRecorder_ObjectOwner_ObjectOwnerID",
                        column: x => x.ObjectOwnerID,
                        principalTable: "ObjectOwner",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Articule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ArhivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OutStockAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UnitID = table.Column<int>(type: "int", nullable: true),
                    CodeUKTZEDID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_CodeUKTZED_CodeUKTZEDID",
                        column: x => x.CodeUKTZEDID,
                        principalTable: "CodeUKTZED",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Product_Discount_DiscountID",
                        column: x => x.DiscountID,
                        principalTable: "Discount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Product_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutomaticLogin = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserRoleID = table.Column<int>(type: "int", nullable: true),
                    SignatureKeyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRole",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_UserSignatureKey_SignatureKeyID",
                        column: x => x.SignatureKeyID,
                        principalTable: "UserSignatureKey",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OperationsRecorderAndUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpertionsRecordersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MediaAccessControls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    WorkingShiftsID = table.Column<int>(type: "int", nullable: true),
                    OperationsRecorderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAccessControls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MediaAccessControls_OperationsRecorder_OperationsRecorderID",
                        column: x => x.OperationsRecorderID,
                        principalTable: "OperationsRecorder",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WorkingShift",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalNumberRRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactoryNumberRRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPacketIdentifier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeRRO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeShiftCrateAt = table.Column<int>(type: "int", nullable: false),
                    TypeShiftEndAt = table.Column<int>(type: "int", nullable: false),
                    TotalCheckForShift = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalReturnCheckForShift = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfOfficialFundsReceivedCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfOfficialFundsIssuedCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfOfficialFundsReceivedCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfOfficialFundsIssuedCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfFundsReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfFundsIssued = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MACIdCreateAt = table.Column<int>(type: "int", nullable: true),
                    MACIdEndAt = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserOpenShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserCloseShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingShift", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkingShift_MediaAccessControls_MACIdCreateAt",
                        column: x => x.MACIdCreateAt,
                        principalTable: "MediaAccessControls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkingShift_MediaAccessControls_MACIdEndAt",
                        column: x => x.MACIdEndAt,
                        principalTable: "MediaAccessControls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkingShift_User_UserCloseShiftID",
                        column: x => x.UserCloseShiftID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WorkingShift_User_UserOpenShiftID",
                        column: x => x.UserOpenShiftID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePayment = table.Column<int>(type: "int", nullable: false),
                    TypeOperation = table.Column<int>(type: "int", nullable: false),
                    BuyersAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RestPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodsTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountOfFundsReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountOfIssuedFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MACId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Operation_MediaAccessControls_MACId",
                        column: x => x.MACId,
                        principalTable: "MediaAccessControls",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Operation_WorkingShift_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "WorkingShift",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OperationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Operation_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Order_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaAccessControls_OperationsRecorderID",
                table: "MediaAccessControls",
                column: "OperationsRecorderID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAccessControls_WorkingShiftsID",
                table: "MediaAccessControls",
                column: "WorkingShiftsID");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_MACId",
                table: "Operation",
                column: "MACId",
                unique: true,
                filter: "[MACId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_ShiftID",
                table: "Operation",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsRecorder_ObjectOwnerID",
                table: "OperationsRecorder",
                column: "ObjectOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsRecorderAndUser_OpertionsRecordersID",
                table: "OperationsRecorderAndUser",
                column: "OpertionsRecordersID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsRecorderAndUser_UsersID",
                table: "OperationsRecorderAndUser",
                column: "UsersID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OperationID",
                table: "Order",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductID",
                table: "Order",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CodeUKTZEDID",
                table: "Product",
                column: "CodeUKTZEDID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DiscountID",
                table: "Product",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitID",
                table: "Product",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_User_SignatureKeyID",
                table: "User",
                column: "SignatureKeyID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleID",
                table: "User",
                column: "UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserID",
                table: "UserToken",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingShift_MACIdCreateAt",
                table: "WorkingShift",
                column: "MACIdCreateAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingShift_MACIdEndAt",
                table: "WorkingShift",
                column: "MACIdEndAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingShift_UserCloseShiftID",
                table: "WorkingShift",
                column: "UserCloseShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingShift_UserOpenShiftID",
                table: "WorkingShift",
                column: "UserOpenShiftID");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaAccessControls_WorkingShift_WorkingShiftsID",
                table: "MediaAccessControls",
                column: "WorkingShiftsID",
                principalTable: "WorkingShift",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaAccessControls_OperationsRecorder_OperationsRecorderID",
                table: "MediaAccessControls");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaAccessControls_WorkingShift_WorkingShiftsID",
                table: "MediaAccessControls");

            migrationBuilder.DropTable(
                name: "GiftCertificates");

            migrationBuilder.DropTable(
                name: "OperationsRecorderAndUser");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "CodeUKTZED");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "OperationsRecorder");

            migrationBuilder.DropTable(
                name: "ObjectOwner");

            migrationBuilder.DropTable(
                name: "WorkingShift");

            migrationBuilder.DropTable(
                name: "MediaAccessControls");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserSignatureKey");
        }
    }
}
