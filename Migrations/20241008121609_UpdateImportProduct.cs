using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImportProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "ImportProducts",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Users",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ImportProducts",
                newName: "UnitPrice");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Users",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");
        }
    }
}
