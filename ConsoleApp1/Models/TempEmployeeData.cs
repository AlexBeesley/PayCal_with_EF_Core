namespace PayCal.Models
{
    public class TempEmployeeData : EmployeeData
    {
        public int? DayRateint { get; set; }
        public int? WeeksWorkedint { get; set; }

        public override string ToString()
        {
            return $"\nID: {EmployeeID} Name: {FName} {LName} Day Rate: £{DayRateint} Weeks Worked: {WeeksWorkedint}";
        }
    }
}
