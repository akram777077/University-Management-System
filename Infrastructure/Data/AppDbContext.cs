using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Enrollment> Enrollments { get; set; }
        DbSet<Instructor> Instructors { get; set; }
        DbSet<Office> Offices { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<SectionSchedule> SectionSchedules { get; set; }
        DbSet<Student> Students { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
