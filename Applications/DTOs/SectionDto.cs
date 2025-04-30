namespace Applications.DTOs
{
    public record SectionDto
    {
        public int Id { get; init; }
        public string? SectionName { get; init; }
        public int CourseId { get; init; }
        public CourseDto? Course { get; init; }
        public int? InstructorId { get; init; }
        public InstructorDto? Instructor { get; init; }

        public ICollection<StudentDto> Students { get; init; } = new List<StudentDto>();
        public ICollection<ScheduleDto> Schedules { get; init; } = new List<ScheduleDto>();
        public ICollection<SectionScheduleDto> SectionSchedules { get; init; } = new List<SectionScheduleDto>();
    }
}


