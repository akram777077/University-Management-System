namespace Applications.DTOs
{
    public record InstructorDto
    {
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public int? OfficeId { get; set; }
        public OfficeDto? Office { get; set; }

        public ICollection<SectionDto> Sections { get; set; } = new List<SectionDto>();
    }
}
