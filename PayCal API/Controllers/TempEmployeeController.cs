using Microsoft.AspNetCore.Mvc;
using PayCal;

namespace PayCal_API.Controllers
{
    public class TempEmployeeController : Controller
    {
        private TempEmployeeRepository temp;
        private Calculator cal;

        public TempEmployeeController(IRepository<TempEmployeeData> Temp)
        {
            temp = (TempEmployeeRepository)Temp;
            cal = new Calculator(null, temp);
        }

        [HttpGet("~/Temporary/All-Employees")]
        public string GetAllTempEmployees()
        {
            return ($"{(string.Concat(temp.ReadAll()))}");
        }

        [HttpGet("~/Temporary/Employee")]
        public string GetTempEmployee(int ID)
        {
            return (string.Concat(temp.Read(ID)));
        }

        [HttpGet("~/Temporary/Employee/ID")]
        public int GetTempEmployeeID(int ID)
        {
            return (temp.Read(ID).EmployeeID);
        }

        [HttpGet("~/Temporary/Employee/Employment-Type")]
        public string GetTempEmploymentType(int ID)
        {
            return ($"Employee with ID: {temp.Read(ID).EmployeeID} is Temporary");
        }

        [HttpGet("~/Temporary/Employee/Full-Name")]
        public string GetTempEmployeeFullName(int ID)
        {
            return ($"{temp.Read(ID).FName} {temp.Read(ID).LName}");
        }

        [HttpGet("~/Temporary/Employee/Day-Rate")]
        public int? GetTempDayRate(int ID)
        {
            return (temp.Read(ID).DayRateint);
        }

        [HttpGet("~/Temporary/Employee/WeeksWorked")]
        public int? GetTempWeeksWorked(int ID)
        {
            return (temp.Read(ID).WeeksWorkedint);
        }

        [HttpPut("~/Temporary/NewEmployee")]
        public bool PutNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            try
            {
                temp.Create(fname, lname, dayrate, weeksworked);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("~/Temporary/Delete-Employee")]
        public bool DeleteTempEmployee(int ID)
        {
            temp.Delete(ID);
            return true;
        }

        [HttpGet("~/Temporary/Employee/Gross-Income")]
        public double GetTempGrossIncome(int ID)
        {
            return (cal.CalculateEmployeePay(ID).Item1);
        }

        [HttpGet("~/Temporary/Employee/Income-After-Tax")]
        public double GetTempIncomeAfterTax(int ID)
        {
            return (cal.CalculateEmployeePay(ID).Item2);
        }
    }
}
