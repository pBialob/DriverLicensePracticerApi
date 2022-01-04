using DriverLicensePracticerApi.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DriverLicensePracticerApi.Seeders
{
    public class QuestionSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        public QuestionSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Seed(string questionBasePath)
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Questions.Any())
                {
                    var restaurants = GetQuestions(questionBasePath);
                    _dbContext.Questions.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private  List<Question> GetQuestions(string path)
        {
            var questions = new List<Question>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = File.Open(path: path, FileMode.Open, FileAccess.Read))
            {

                using (var package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        questions.Add(new Question()
                        {
                            QuestionName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            QuestionNumber = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            QuestionContent = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            AnswerA = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            AnswerB = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            AnswerC = worksheet.Cells[row, 6].Value.ToString().Trim(),
                            CorrectAnswer = worksheet.Cells[row, 15].Value.ToString().Trim(),
                            MediaPath = worksheet.Cells[row, 16].Value.ToString().Trim(),
                            QuestionLevel = worksheet.Cells[row, 17].Value.ToString().Trim(),
                            Points = worksheet.Cells[row, 18].Value.ToString().Trim(),
                            CategoriesToSet = worksheet.Cells[row, 19].Value.ToString().Trim(),
                            QuestionOrigin = worksheet.Cells[row, 21].Value.ToString().Trim(),
                            QuestionReason = worksheet.Cells[row, 22].Value.ToString().Trim(),
                            SafetyExplanation = worksheet.Cells[row, 23].Value.ToString().Trim()

                        });
                    }
                }
            }
            return questions;
        }
    }
}