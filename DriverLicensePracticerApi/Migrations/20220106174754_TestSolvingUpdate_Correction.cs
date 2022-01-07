using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriverLicensePracticerApi.Migrations
{
    public partial class TestSolvingUpdate_Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Users_UserId1",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_UserId1",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Tests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserId1",
                table: "Tests",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Users_UserId1",
                table: "Tests",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
