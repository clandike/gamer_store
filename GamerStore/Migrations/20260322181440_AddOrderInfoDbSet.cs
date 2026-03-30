using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerStore.Migrations
{
    public partial class AddOrderInfoDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_Orders_OrderId",
                table: "OrderInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfo",
                table: "OrderInfo");

            migrationBuilder.RenameTable(
                name: "OrderInfo",
                newName: "OrderInfos");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfos_Orders_OrderId",
                table: "OrderInfos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfos_Orders_OrderId",
                table: "OrderInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos");

            migrationBuilder.RenameTable(
                name: "OrderInfos",
                newName: "OrderInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "OrderInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfo",
                table: "OrderInfo",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_Orders_OrderId",
                table: "OrderInfo",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
