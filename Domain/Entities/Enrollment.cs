namespace Domain.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
        public Student Student { get; set; } = null!;

    }
}
