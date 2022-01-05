using Microsoft.AspNetCore.Mvc;
using PayCal;

namespace PayCal_API.Controllers
{
    public class PermEmployeeController : Controller
    {
        private PermEmployeeRepository perm;
        private Calculator cal;

        public PermEmployeeController(IRepository<PermEmployeeData> Perm)
        {
            perm = (PermEmployeeRepository)Perm;
            cal = new Calculator(perm, null);
        }

        [HttpGet("~/Permanent/All-Employees")]
        public string GetAllPermEmployees()
        {
            return ($"{(string.Concat(perm.ReadAll()))}");
        }

        [HttpGet("~/Permanent/Employee")]
        public string GetPermEmployee(int ID)
        {
            return (string.Concat(perm.Read(ID)));
        }

        [HttpGet("~/Permanent/Employee/ID")]
        public int GetPermEmployeeID(int ID)
        {
            return (perm.Read(ID).EmployeeID);
        }

        [HttpGet("~/Permanent/Employee/Employment-Type")]
        public string GetPermEmploymentType(int ID)
        {
            return ($"Employee with ID: {perm.Read(ID).EmployeeID} is Permanent");
        }

        [HttpGet("~/Permanent/Employee/Full-Name")]
        public string GetPermEmployeeFullName(int ID)
        {
            return ($"{perm.Read(ID).FName} {perm.Read(ID).LName}");
        }

        [HttpGet("~/Permanent/Employee/Salary")]
        public int? GetPermSalary(int ID)
        {
            return (perm.Read(ID).Salaryint);
        }

        [HttpGet("~/Permanent/Employee/Bonus")]
        public int? GetPermBonus(int ID)
        {
            return (perm.Read(ID).Bonusint);
        }

        [HttpPut("~/Permanent/NewEmployee")]
        public bool PutNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            try
            {
                perm.Create(fname, lname, salary, bonus);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("~/Permanent/Delete-Employee")]
        public bool DeletePermEmployee(int ID)
        {
            perm.Delete(ID);
            return true;
        }

        [HttpGet("~/Permanent/Employee/Gross-Income")]
        public double GetPermGrossIncome(int ID)
        {
            return (cal.CalculateEmployeePay(ID).Item1);
        }

        [HttpGet("~/Permanent/Employee/Income-After-Tax")]
        public double GetPermIncomeAfterTax(int ID)
        {
            return (cal.CalculateEmployeePay(ID).Item2);
        }
    }
}
