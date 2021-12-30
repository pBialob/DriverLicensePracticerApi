using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverLicensePracticerApi.Migrations
{
    public partial class InitQuestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafetyExplanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
