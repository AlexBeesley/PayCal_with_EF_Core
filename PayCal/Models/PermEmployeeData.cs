using System.ComponentModel.DataAnnotations;

namespace PayCal.Models
{
    public class PermEmployeeData : EmployeeData
    {
        [Required] public int Salaryint { get; set; }
        [Required] public int Bonusint { get; set; }

        public override string ToString()
        {
            return $"\nID: {EmployeeID} Name: {FName} {LName} Salary: £{Salaryint} Bonus: £{Bonusint}";
        }
    }
}
