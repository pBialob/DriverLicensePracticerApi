using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriverLicensePracticerApi.Migrations
{
    public partial class UpdateAnswerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "QuestionNumber",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionNumber",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
