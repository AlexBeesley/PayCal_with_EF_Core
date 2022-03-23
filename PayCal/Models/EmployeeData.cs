using System.ComponentModel.DataAnnotations;

namespace PayCal.Models
{
    public abstract class EmployeeData
    {
        public int EmployeeID { get; set; }
        [Required]
        public string? FName { get; set; }
        public string? LName { get; set; }
    }
}
