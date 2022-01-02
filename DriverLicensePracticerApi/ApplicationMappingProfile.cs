using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;

namespace DriverLicensePracticerApi
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Question, QuestionDto>();

        }
    }
}
