using Hospital.Models;

public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string TC { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? Disease { get; set; }
    public string? MedicalRecord { get; set; }
    public string? Medicine { get; set; }
    public string UserType { get; set; } = "Patient";
    public int? DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
} 