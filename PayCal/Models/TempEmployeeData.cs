using System.ComponentModel.DataAnnotations;

namespace PayCal.Models
{
    public class TempEmployeeData : EmployeeData
    {
        [Required] public int DayRateint { get; set; }
        [Required] public int WeeksWorkedint { get; set; }

        public override string ToString()
        {
            return $"\nID: {EmployeeID} Name: {FName} {LName} Day Rate: £{DayRateint} Weeks Worked: {WeeksWorkedint}";
        }
    }
}
