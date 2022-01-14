namespace PayCal.Services
{
    public interface ICalculator
    {
        (double, double) CalculateEmployeePay(int employeeID);
    }
}
