using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public string TC { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        public int BranchId { get; set; }
        
        [Required]
        public string UserName { get; set; } = string.Empty;
        
        public string UserType { get; set; } = "Doctor";

        // Navigation property
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
} 