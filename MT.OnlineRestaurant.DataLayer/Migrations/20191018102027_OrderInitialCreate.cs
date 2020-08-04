using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class OrderInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoggingInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true, defaultValueSql: "('')"),
                    ControllerName = table.Column<string>(maxLength: 250, nullable: true, defaultValueSql: "('')"),
                    ActionName = table.Column<string>(maxLength: 250, nullable: true, defaultValueSql: "('')"),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggingInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblFoodOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblCustomerID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblRestaurantID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblOrderStatusID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblPaymentTypeID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFoodOrder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblFoodOrder_tblOrderStatusID",
                        column: x => x.tblOrderStatusID,
                        principalTable: "tblOrderStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFoodOrder_tblPaymentTypeID",
                        column: x => x.tblPaymentTypeID,
                        principalTable: "tblPaymentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTableOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblCustomerID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblRestaurantID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    tblOrderStatusID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblPaymentTypeID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTableOrder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblTableOrder_tblOrderStatusID",
                        column: x => x.tblOrderStatusID,
                        principalTable: "tblOrderStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblTableOrder_tblPaymentTypeID",
                        column: x => x.tblPaymentTypeID,
                        principalTable: "tblPaymentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFoodOrderMapping",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblFoodOrderID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblMenuID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFoodOrderMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblFoodOrderMapping_tblFoodOrderID",
                        column: x => x.tblFoodOrderID,
                        principalTable: "tblFoodOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderPayment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblFoodOrderID = table.Column<int>(nullable: false),
                    tblPaymentTypeID = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    TransactionID = table.Column<string>(maxLength: 20, nullable: false, defaultValueSql: "('0000000000')"),
                    tblCustomerID = table.Column<int>(nullable: false),
                    tblPaymentStatusID = table.Column<int>(nullable: false),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblFoodOrderID",
                        column: x => x.tblFoodOrderID,
                        principalTable: "tblFoodOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblPaymentStatusID",
                        column: x => x.tblPaymentStatusID,
                        principalTable: "tblPaymentStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblPaymentType",
                        column: x => x.tblPaymentTypeID,
                        principalTable: "tblPaymentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTableOrderMapping",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblTableOrderID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    TableNo = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTableOrderMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblTableOrderMapping_tblTableOrderID",
                        column: x => x.tblTableOrderID,
                        principalTable: "tblTableOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFoodOrder_tblOrderStatusID",
                table: "tblFoodOrder",
                column: "tblOrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFoodOrder_tblPaymentTypeID",
                table: "tblFoodOrder",
                column: "tblPaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFoodOrderMapping_tblFoodOrderID",
                table: "tblFoodOrderMapping",
                column: "tblFoodOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderPayment_tblFoodOrderID",
                table: "tblOrderPayment",
                column: "tblFoodOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderPayment_tblPaymentStatusID",
                table: "tblOrderPayment",
                column: "tblPaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderPayment_tblPaymentTypeID",
                table: "tblOrderPayment",
                column: "tblPaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTableOrder_tblOrderStatusID",
                table: "tblTableOrder",
                column: "tblOrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTableOrder_tblPaymentTypeID",
                table: "tblTableOrder",
                column: "tblPaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTableOrderMapping_tblTableOrderID",
                table: "tblTableOrderMapping",
                column: "tblTableOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggingInfo");

            migrationBuilder.DropTable(
                name: "tblFoodOrderMapping");

            migrationBuilder.DropTable(
                name: "tblOrderPayment");

            migrationBuilder.DropTable(
                name: "tblTableOrderMapping");

            migrationBuilder.DropTable(
                name: "tblFoodOrder");

            migrationBuilder.DropTable(
                name: "tblPaymentStatus");

            migrationBuilder.DropTable(
                name: "tblTableOrder");

            migrationBuilder.DropTable(
                name: "tblOrderStatus");

            migrationBuilder.DropTable(
                name: "tblPaymentType");
        }
    }
}
