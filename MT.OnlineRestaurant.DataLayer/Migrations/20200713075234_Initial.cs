using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "tblCartItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tblCustomerID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblRestaurantID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    tblMenuID = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    DeliveryAddress = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    IsItemAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCartItems", x => x.ID);
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
        }
    }
}
