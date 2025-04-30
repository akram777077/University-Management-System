namespace Applications.DTOs
{
    public record OfficeDto
    {
        public int Id { get; set; }
        public string? OfficeName { get; set; }
        public string? OfficeLocation { get; set; }

        public InstructorDto? Instructor { get; set; }
    }
}

