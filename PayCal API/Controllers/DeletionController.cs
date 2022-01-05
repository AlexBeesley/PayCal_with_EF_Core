using Microsoft.AspNetCore.Mvc;
using PayCal;
using PayCal_API.Services;

namespace PayCal_API.Controllers
{
    public class DeletionController : Controller
    {
        public static IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
        public static IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
        DeletionService service = new DeletionService(Perm: perm, Temp: temp);

        [HttpPut("~/Delete-Employee")]
        public bool GetGrossIncome(int ID) 
        {
            return service.DeleteEmployee(ID);
        }
    } 
}
 