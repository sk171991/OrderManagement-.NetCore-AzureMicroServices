using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class ordernew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblItemOutOfStock",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TblRestaurantId = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblMenuID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    IsItemAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemOutOfStock", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItemOutOfStock");
        }
    }
}
