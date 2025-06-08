using AutoMapper;

namespace Applications.Mappers
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyStudentMapping();
            ApplyCountryMapping();
            ApplyPersonMapping();
        }

        //Method signatures
        partial void ApplyStudentMapping();
        partial void ApplyCountryMapping();
        partial void ApplyPersonMapping();
    }

  
}
