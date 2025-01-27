namespace Hospital.Models.ViewModels
{
    public class PatientDashboardViewModel
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TC { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Disease { get; set; }
        public string? MedicalRecord { get; set; }
        public string? Medicine { get; set; }
    }
} 