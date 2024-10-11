using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class AddManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsSeniorManager = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManagerId",
                table: "Orders",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ManagerId",
                table: "Invoices",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Managers_ManagerId",
                table: "Invoices",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Managers_ManagerId",
                table: "Orders",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Managers_ManagerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Managers_ManagerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ManagerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ManagerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Invoices");
        }
    }
}
