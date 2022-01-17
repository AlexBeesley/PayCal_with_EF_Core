using PayCal.Models;
using PayCal.Repositories;

namespace PayCal.Services
{
    public class Calculator : ICalculator
    {
        public double AnnualPayAfterTax;
        public double AnnualPay;

        private readonly IRepository<PermEmployeeData> _perm;
        private readonly IRepository<TempEmployeeData> _temp;

        public Calculator(IRepository<PermEmployeeData> perm, IRepository<TempEmployeeData> temp)
        {
            _perm = perm;
            _temp = temp;
        }

        public double CalculateEmployeePay(int employeeID)
        {
            var tempDate = _temp.Read(employeeID);
            var permData = _perm.Read(employeeID);

            if (tempDate == null)
            { 
                int Salary = (int)permData.Salaryint;
                int Bonus = (int)permData.Bonusint;
                AnnualPay = Salary + Bonus;
            }
            else
            {
                int DayRate = (int)tempDate.DayRateint;
                int WeeksWorked = (int)tempDate.WeeksWorkedint;
                AnnualPay = (DayRate * 5) + WeeksWorked;
            }

            AnnualPayAfterTax = AnnualPay * CalculateTaxBands(AnnualPay);
            return AnnualPayAfterTax;
        }

        public double CalculateTaxBands(double grossIncome)
        {
            double percentageTax = 0;

            if (grossIncome >= 11851 && grossIncome <= 46350)
            {
                percentageTax = 0.2;
            }
            else if (grossIncome >= 46351 && grossIncome <= 150000)
            {
                percentageTax = 0.4;
            }
            else if (grossIncome > 150000)
            {
                percentageTax = 0.45;
            }

            return percentageTax;
        }
    }
}

