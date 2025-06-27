using Applications.DTOs.Enrollment;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappers;

public partial class MappingProfile : Profile
{
    partial void ApplyEnrollmentMapping()
    {
        CreateMap<EnrollmentRequest, Enrollment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EnrollmentDate, opt => opt.Condition(src => src.EnrollmentDate.HasValue))
            .ForMember(dest => dest.GraduationDate, opt => opt.Condition(src => src.GraduationDate.HasValue))
            .ForMember(dest => dest.Notes, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Notes)))
            .ForMember(dest => dest.StudentId, opt => opt.Condition(src => src.StudentId.HasValue))
            .ForMember(dest => dest.ProgramId, opt => opt.Condition(src => src.ProgramId.HasValue))
            .ForMember(dest => dest.ServiceApplicationId, opt => opt.Condition(src => src.ServiceApplicationId.HasValue))
            .ForMember(dest => dest.Program, opt => opt.Ignore())
            .ForMember(dest => dest.Student, opt => opt.Ignore())
            .ForMember(dest => dest.ServiceApplication, opt => opt.Ignore());

        
        CreateMap<Enrollment, EnrollmentResponse>()
            .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.Program != null ? src.Program.Name : null));
    }
}