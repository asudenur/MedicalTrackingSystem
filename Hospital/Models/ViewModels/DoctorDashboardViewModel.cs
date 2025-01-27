namespace Hospital.Models.ViewModels
{
    public class DoctorDashboardViewModel
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TC { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
} 