using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class RelationEmployeeWorkPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkPositionId",
                table: "Employees",
                column: "WorkPositionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_WorkPositions_WorkPositionId",
                table: "Employees",
                column: "WorkPositionId",
                principalTable: "WorkPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_WorkPositions_WorkPositionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkPositionId",
                table: "Employees");
        }
    }
}
