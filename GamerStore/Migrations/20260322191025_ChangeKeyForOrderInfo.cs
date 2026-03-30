using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerStore.Migrations
{
    public partial class ChangeKeyForOrderInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos",
                columns: new[] { "OrderId", "ProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos",
                column: "OrderId");
        }
    }
}
