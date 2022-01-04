using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using System.Linq;

namespace DriverLicensePracticerApi
{
    public interface IApplicationMappingProfile
    {
        void MapCategoryName(QuestionDto question);
    }

    public class ApplicationMappingProfile : Profile, IApplicationMappingProfile
    {
        private readonly ApplicationDbContext _context;
        public ApplicationMappingProfile(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public ApplicationMappingProfile()
        {
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionCategory, CategoryDto>();

        }
        public void MapCategoryName(QuestionDto question)
        {
            foreach (var category in question.Categories)
            {
                category.Name = _context.Categories.FirstOrDefault(c => c.Id == category.CategoryId).Name;
            }
        }
    }
}
