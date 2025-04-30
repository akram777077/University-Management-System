using Applications.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappers
{
    public partial class MappingProfile : Profile
    {
        partial void ApplyStudentMapping()
        {
            CreateMap<Student, StudentDto>().ReverseMap();

            CreateMap<StudentDto, Student>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
