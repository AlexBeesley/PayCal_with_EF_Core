using PayCal;

namespace PayCal_API.Services
{
    public class CalculatorService
    {
        private PermEmployeeRepository perm;
        private TempEmployeeRepository temp;
        private Calculator cal;

        public CalculatorService(IRepository<PermEmployeeData> Perm, IRepository<TempEmployeeData> Temp)
        {
            perm = (PermEmployeeRepository)Perm;
            temp = (TempEmployeeRepository)Temp;
            cal = new Calculator(perm, temp);
        }

        public double CalculateGrossIncome(int ID)
        {
            return (cal.CalculateEmployeePay(ID).Item1);
        }

        public double CalculatIncomeAfterTax(int ID)
        {
            return (cal.CalculateEmployeePay(ID).Item2);
        }
    }
}
