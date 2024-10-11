using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Users",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Customers",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
