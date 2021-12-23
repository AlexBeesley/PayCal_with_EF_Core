namespace PayCal
{
    public class Calculator
    {
        public double AnnualPayAfterTax;
        public double AnnualPay;

        private readonly PermEmployeeRepository permRE;
        private readonly TempEmployeeRepository tempRE;

        public Calculator(IRepository<PermEmployeeData> permRepo, IRepository<TempEmployeeData> tempRepo)
        {
            permRE = (PermEmployeeRepository)permRepo;
            tempRE = (TempEmployeeRepository)tempRepo;
        }

        public double CalculateEmployeePay(int employeeID)
        {
            try 
            {
                int Salary = (int)permRE.Read(employeeID).Salaryint;
                int Bonus = (int)permRE.Read(employeeID).Bonusint;
                AnnualPay = Salary + Bonus;
            }
            catch
            {
                int DayRate = (int)tempRE.Read(employeeID).DayRateint;
                int WeeksWorked = (int)tempRE.Read(employeeID).WeeksWorkedint;
                AnnualPay = (DayRate * 5) + WeeksWorked;
            }

            if (AnnualPay < 12570) { AnnualPayAfterTax = AnnualPay; }
            if (AnnualPay > 12570) { AnnualPayAfterTax = (AnnualPay - 12570) * 0.2; }
            return (AnnualPayAfterTax);
        }
    }
}

