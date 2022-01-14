using PayCal.Models;
using PayCal.Repositories;

namespace PayCal.Services
{
    public class Calculator : ICalculator
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

        public (double, double) CalculateEmployeePay(int employeeID)
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

            AnnualPayAfterTax = AnnualPay * CalculateTaxBands(AnnualPay);
            return (AnnualPay, AnnualPayAfterTax);
        }

        public static double CalculateTaxBands(double grossIncome)
        {
            double percentageTax = 0;

            if (grossIncome >= 11851 && grossIncome <= 46350)
            {
                percentageTax = 0.20;
            }
            else if (grossIncome >= 46351 && grossIncome <= 150000)
            {
                percentageTax = 0.40;
            }
            else if (grossIncome > 150000)
            {
                percentageTax = 0.45;
            }

            return percentageTax;
        }
    }
}

