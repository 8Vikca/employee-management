using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class historyModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkPositionsHistory_EmployeeId",
                table: "WorkPositionsHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPositionsHistory_WorkPositionId",
                table: "WorkPositionsHistory",
                column: "WorkPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPositionsHistory_Employees_EmployeeId",
                table: "WorkPositionsHistory",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPositionsHistory_WorkPositions_WorkPositionId",
                table: "WorkPositionsHistory",
                column: "WorkPositionId",
                principalTable: "WorkPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPositionsHistory_Employees_EmployeeId",
                table: "WorkPositionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPositionsHistory_WorkPositions_WorkPositionId",
                table: "WorkPositionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_WorkPositionsHistory_EmployeeId",
                table: "WorkPositionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_WorkPositionsHistory_WorkPositionId",
                table: "WorkPositionsHistory");
        }
    }
}
