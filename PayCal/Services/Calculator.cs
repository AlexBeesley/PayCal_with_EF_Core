using PayCal.Models;
using PayCal.Repositories;

namespace PayCal.Services
{
    public class Calculator : ICalculator
    {
        private readonly IRepository<PermEmployeeData> _perm;
        private readonly IRepository<TempEmployeeData> _temp;

        public Calculator(IRepository<PermEmployeeData> perm, IRepository<TempEmployeeData> temp)
        {
            _perm = perm;
            _temp = temp;
        }

        public double AnnualPayAfterTax;
        public double AnnualPay;
        public double income;

        public double CalculateEmployeePay(string employeeID)
        {
            var permEmployee = _perm.Read(employeeID);
            
            if (permEmployee is null)
            {
                var tempEmployee = _temp.Read(employeeID);
                income = tempEmployee.WeeksWorkedint * tempEmployee.DayRateint;
            }
            if (permEmployee is not null)
            {
                income = permEmployee.Salaryint + permEmployee.Bonusint;
            }

            double tax = 0;
            if (income <= 18200)
            {
                tax = 0;
            }
            else if (income <= 37000)
            {
                tax = (income - 18200) * 0.19;
            }
            else if (income <= 87000)
            {
                tax = 3572 + (income - 37000) * 0.325;
            }
            else if (income <= 180000)
            {
                tax = 19822 + (income - 87000) * 0.37;
            }
            else
            {
                tax = 54232 + (income - 180000) * 0.45;
            }
            
            return income - tax;
        }
    }
}

