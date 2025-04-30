namespace Applications.DTOs
{
    public record EnrollmentDto
    {
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public SectionDto Section { get; set; } = null!;
        public StudentDto Student { get; set; } = null!;
    }
}

