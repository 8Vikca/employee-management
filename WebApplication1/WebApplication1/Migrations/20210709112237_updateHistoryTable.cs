using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class updateHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WorkPositionId",
                table: "WorkPositionsHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "WorkPositionsHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "WorkPositions",
                type: "int",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPositionsHistory_WorkPositions_WorkPositionId",
                table: "WorkPositionsHistory",
                column: "WorkPositionId",
                principalTable: "WorkPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "WorkPositions");

            migrationBuilder.AlterColumn<int>(
                name: "WorkPositionId",
                table: "WorkPositionsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "WorkPositionsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
