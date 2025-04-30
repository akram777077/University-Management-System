namespace Applications.DTOs
{
    public record CourseDto
    {
        public int Id { get; init; }
        public string? CourseName { get; init; }
        public decimal Price { get; init; }
        public ICollection<SectionDto> Sections { get; init; } = new List<SectionDto>();
    }
}


