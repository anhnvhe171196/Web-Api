using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class updateinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportProducts_Products_ProductId",
                table: "ImportProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportProducts_Products_ProductId",
                table: "ImportProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportProducts_Products_ProductId",
                table: "ImportProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportProducts_Products_ProductId",
                table: "ImportProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
