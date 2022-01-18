using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi
{

    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Question, QuestionDto>()
                .ForMember(x => x.Categories, x => x.MapFrom(x => x.CategoriesToSet));

            CreateMap<Answer, AnswerDto>();

            CreateMap<Test, TestDto>()
                .ForMember(x => x.TestId, x => x.MapFrom(x => x.Id));
        }
    }
}
