namespace Hospital.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TC { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public int? BranchId { get; set; }
        public int? Age { get; set; }
    }
} 