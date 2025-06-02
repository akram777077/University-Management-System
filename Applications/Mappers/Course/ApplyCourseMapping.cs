using Applications.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappers;

public partial class MappingProfile : Profile
{
    partial void ApplyCourseMapping()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        
        CreateMap<CourseDto, Course>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<CourseDto, Course>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}