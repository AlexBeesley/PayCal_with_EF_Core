public class PermEmployeeData : EmployeeData
{
    public int? Salaryint { get; set; }
    public int? Bonusint { get; set; }

    public override string ToString()
    {
        return $"\nID: {EmployeeID} Name: {FName} {LName} Salary: £{Salaryint} Bonus: £{Bonusint}";
    }
}