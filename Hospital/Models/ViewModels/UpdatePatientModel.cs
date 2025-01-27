namespace Hospital.Models.ViewModels
{
    public class UpdatePatientModel
    {
        public int PatientId { get; set; }
        public string? Disease {  get; set; }
        public string? MedicalRecord { get; set; }
        public string? Medicine { get; set; }
    }
}
