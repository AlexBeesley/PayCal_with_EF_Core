using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayCal.Models
{
    public abstract class EmployeeData
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EmployeeID { get; set; }

        [Required] public string FName { get; set; }

        [Required] public string LName { get; set; }
    }
}
