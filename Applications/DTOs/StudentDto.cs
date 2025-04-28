using Domain.Entities;

namespace Applications.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();

        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {FName}, LastName: {LName}";
        }
    }
}
