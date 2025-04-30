namespace Applications.DTOs
{
    public record ScheduleDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool SUN { get; set; }
        public bool MON { get; set; }
        public bool TUE { get; set; }
        public bool WED { get; set; }
        public bool THU { get; set; }
        public bool FRI { get; set; }
        public bool SAT { get; set; }


        public ICollection<SectionDto> Sections { get; set; } = new List<SectionDto>();
        public ICollection<SectionScheduleDto> SectionSchedules { get; set; } = new List<SectionScheduleDto>();
    }
}

