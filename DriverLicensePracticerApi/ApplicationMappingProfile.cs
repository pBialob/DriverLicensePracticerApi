using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using System.Linq;

namespace DriverLicensePracticerApi
{

    public class ApplicationMappingProfile : Profile
    {
        private readonly ApplicationDbContext _context;
        public ApplicationMappingProfile(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public ApplicationMappingProfile()
        {
            CreateMap<Question, QuestionDto>()
                .ForMember(x => x.Categories, x => x.MapFrom(x => x.CategoriesToSet));
        }
    }
}
