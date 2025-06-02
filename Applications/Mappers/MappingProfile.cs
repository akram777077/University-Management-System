using AutoMapper;

namespace Applications.Mappers
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyStudentMapping();
            ApplyCourseMapping();
        }

        //Method signatures
        partial void ApplyStudentMapping();
        partial void ApplyCourseMapping();
    }

  
}
