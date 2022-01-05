using Microsoft.AspNetCore.Mvc;
using PayCal;
using PayCal_API.Services;

namespace PayCal_API.Controllers
{
    public class InjectController : Controller
    {
        public static IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
        public static IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
        InjectService service = new InjectService(Perm: perm, Temp: temp);

        [HttpPut("~/NewEmployee")]
        public bool PutNewEmployee(bool isperm, string fname, string lname, int salary_or_dayrate, int bonus_or_weeksworked) 
        {
            return service.InjectNewEmployee(isperm, fname, lname, salary_or_dayrate, bonus_or_weeksworked);
        }
    }
}
