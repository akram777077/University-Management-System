using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.SectionId });
    
            builder.ToTable("Enrollments");

            builder.HasData(LoadEnrollments());
        }

        private List<Enrollment> LoadEnrollments()
        {
            return new List<Enrollment>
            {
                new Enrollment() { StudentId = 1, SectionId = 6 },
                new Enrollment() { StudentId = 2, SectionId = 6 },
                new Enrollment() { StudentId = 3, SectionId = 7 },
                new Enrollment() { StudentId = 4, SectionId = 7 },
                new Enrollment() { StudentId = 5, SectionId = 8 },
                new Enrollment() { StudentId = 6, SectionId = 8 },
                new Enrollment() { StudentId = 7, SectionId = 9 },
                new Enrollment() { StudentId = 8, SectionId = 9 },
                new Enrollment() { StudentId = 9, SectionId = 10 },
                new Enrollment() { StudentId = 10, SectionId = 10 }
            };
        }
    }

}
