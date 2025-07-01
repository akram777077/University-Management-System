namespace Applications.DTOs.Registration;

public record struct RegistrationResponse
{
    public int Id { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal RegistrationFees { get; set; }
    public int StudentNumber { get; set; }
    public string SectionNumber { get; set; }
    public int SemesterTermCode { get; set; }
    public int? ProcessedByUserId { get; set; }
}