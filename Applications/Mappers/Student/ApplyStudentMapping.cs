using Applications.DTOs.Student;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappers
{
    public partial class MappingProfile : Profile
    {
        partial void ApplyStudentMapping()
        {
            CreateMap<Student, StudentResponse>()
           .ForMember(dest => dest.PersonFullName,
               opt => opt.MapFrom(src => src.Person != null
                   ? $"{src.Person.FirstName} {src.Person.LastName}"
                   : string.Empty));

            CreateMap<StudentRequest, Student>()
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, 
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Notes)));
        }
    }
}
