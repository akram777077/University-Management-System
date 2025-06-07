using Applications.DTOs.People;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappers
{
    public partial class MappingProfile : Profile
    {
        partial void ApplyPersonMapping()
        {
            CreateMap<Person, PersonResponse>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.CountryName,
                    opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<PersonRequest, Person>()
                .ForMember(dest => dest.Country, opt => opt.Ignore());
        }
    }
}