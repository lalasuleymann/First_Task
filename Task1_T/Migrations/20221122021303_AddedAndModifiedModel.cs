using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task1_T.Migrations
{
    public partial class AddedAndModifiedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ParentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Employees",
                newName: "EmployeeParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ParentId",
                table: "Employees",
                newName: "IX_Employees_EmployeeParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmployeeParentId",
                table: "Employees",
                column: "EmployeeParentId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmployeeParentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeParentId",
                table: "Employees",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeParentId",
                table: "Employees",
                newName: "IX_Employees_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ParentId",
                table: "Employees",
                column: "ParentId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
