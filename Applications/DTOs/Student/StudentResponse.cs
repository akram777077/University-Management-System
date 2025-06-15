using Domain.Enums;

namespace Applications.DTOs.Student
{
    public record struct StudentResponse
    {
        public int Id { get; set; }
        public required string StudentNumber { get; set; }
        public string Status { get; set; }
        public DateTime EnrollmentDate { get; set; } 
        public DateTime? ExpectedGradDate { get; set; }

        public string PersonFullName { get; set; }
    }
}
