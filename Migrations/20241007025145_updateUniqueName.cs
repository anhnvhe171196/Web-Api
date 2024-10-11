using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class updateUniqueName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_Name",
                table: "ProductDetails",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_Name",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_Category_Name",
                table: "Category");
        }
    }
}
