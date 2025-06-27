namespace Applications.DTOs.Enrollment;

public record struct EnrollmentResponse
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime? GraduationDate { get; set; }
    public int StudentId { get; set; }  
    public string ProgramName { get; set; }
}