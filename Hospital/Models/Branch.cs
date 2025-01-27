using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        
        [Required]
        public string BranchName { get; set; } = string.Empty;
        
        // Navigation property
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
} 