using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.CourseName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(15, 2);
            
            builder.ToTable("Courses");

            builder.HasData(LoadCourses());
        }

        private List<Course> LoadCourses()
        {
            return new List<Course>()
            {
                new Course {Id = 1, CourseName = "Mathematics", Price = 1000m},
                new Course {Id = 2, CourseName = "Physics", Price = 2000m},
                new Course {Id = 3, CourseName = "Chemistry", Price = 1500m},
                new Course {Id = 4, CourseName = "Biology", Price = 1200m},
                new Course {Id = 5, CourseName = "CS-50", Price = 3000m}
            };
        }
    }
}
