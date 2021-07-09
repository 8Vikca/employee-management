using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class RelationEmployeeWorkPositionCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkPositionId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkPositionId",
                table: "Employees",
                column: "WorkPositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkPositionId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkPositionId",
                table: "Employees",
                column: "WorkPositionId",
                unique: true);
        }
    }
}
