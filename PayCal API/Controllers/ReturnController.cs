using Microsoft.AspNetCore.Mvc;
using PayCal;
using PayCal_API.Services;

namespace PayCal_API.Controllers
{
    public class ReturnController : Controller
    {
        public static IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
        public static IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
        ReturnService service = new ReturnService(Perm: perm, Temp: temp);

        [HttpGet("~/All-Employees")]
        public string GetAllEmployees() 
        {
            return service.ReturnAllEmployees();
        }

        [HttpGet("~/Employee")]
        public string GetEmployees(int ID)
        {
            return service.ReturnSingleEmployee(ID);
        }

        [HttpGet("~/Employee/ID")]
        public int GetEmployeeID(int ID)
        {
            return service.ReturnSingleEmployeeID(ID);
        }

        [HttpGet("~/Employee/Employment-Type")]
        public string GetEmploymentType(int ID)
        {
            return service.ReturnEmploymentType(ID);
        }

        [HttpGet("~/Employee/Full-Name")]
        public string GetEmployeeFullName(int ID)
        {
            return service.ReturnFullName(ID);
        }

        [HttpGet("~/Employee/Salary")]
        public int? GetSalary(int ID)
        {
            return service.ReturnSalary(ID);
        }

        [HttpGet("~/Employee/Bonus")]
        public int? GetEmployeeBonus(int ID)
        {
            return service.ReturnBonus(ID);
        }

        [HttpGet("~/Employee/Day-Rate")]
        public int? GetEmployeeDayRate(int ID)
        {
            return service.ReturnDayRate(ID);
        }

        [HttpGet("~/Employee/Weeks-Worked")]
        public int? GetWeeksWorked(int ID)
        {
            return service.ReturnWeeksWorked(ID);
        }
    }
}
