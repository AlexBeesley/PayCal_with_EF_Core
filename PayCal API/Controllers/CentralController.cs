using Microsoft.AspNetCore.Mvc;
using PayCal;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentralController : ControllerBase
    {
        private PermEmployeeRepository perm;
        private TempEmployeeRepository temp;
        private Calculator cal;

        public CentralController(IRepository<PermEmployeeData> Perm, IRepository<TempEmployeeData> Temp)
        {
            perm = (PermEmployeeRepository?)Perm;
            temp = (TempEmployeeRepository?)Temp;
            cal = new Calculator(perm, temp);
        }

        [HttpGet("~/GetEmployees")]
        public string Get()
        {
            return ($"{(string.Concat(perm.ReadAll()))}{string.Concat(temp.ReadAll())}");
        }

        [HttpGet("~/PayCalculator")]
        public string Get(int ID)
        {
            try
            {
                return ($"Employee Name:  {perm.Read(ID).FName} {perm.Read(ID).LName}\nEmployment Type:  Permanent\nAnnual Pay after Tax:  £{cal.CalculateEmployeePay(ID).Item2}");
            }
            catch
            {
                return ($"Employee Name:  {temp.Read(ID).FName} {temp.Read(ID).LName}\nEmployment Type:  Temporary\nAnnual Pay after Tax:  £{cal.CalculateEmployeePay(ID).Item2}");
            }
        }

        [HttpPut("~/AddEmployee")]
        public bool Put(bool isperm, string fname, string lname, int salary_or_dayrate, int bonus_or_weeksworked)
        {
            if (isperm)
            {
                perm.Create(fname, lname, salary_or_dayrate, bonus_or_weeksworked);
            }
            if (!isperm)
            {
                temp.Create(fname, lname, salary_or_dayrate, bonus_or_weeksworked);
            }
            return true;
        }

        [HttpPut("~/DeleteEmployee")]
        public bool Put(int ID)
        {
            bool x = perm.Delete(ID);
            if (!x)
            {
                temp.Delete(ID);
            }
            return true;
        }
    }
}