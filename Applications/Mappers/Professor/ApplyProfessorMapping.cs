using Applications.DTOs.Professor;
using Applications.DTOs.Student;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappers;

public partial class MappingProfile : Profile
{
    partial void ApplyProfessorMapping()
    {
        CreateMap<Professor, ProfessorResponse>()
            .ForMember(dest => dest.AcademicRank, 
                opt => opt.MapFrom(src => src.AcademicRank.ToString()))
            .ForMember(dest => dest.PersonFullName,
                opt => opt.MapFrom(src => src.Person != null
                    ? $"{src.Person.FirstName} {src.Person.LastName}"
                    : string.Empty));

        CreateMap<ProfessorRequest, Professor>()
            .ForMember(dest => dest.Person, opt => opt.Ignore());
    }
}