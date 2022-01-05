using Microsoft.AspNetCore.Mvc;
using PayCal;
using PayCal_API.Services;

namespace PayCal_API.Controllers
{
    public class CalculatorController : Controller
    {
        public static IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
        public static IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
        CalculatorService service = new CalculatorService(Perm: perm, Temp: temp);

        [HttpGet("~/Employee/Gross-Income")]
        public double GetGrossIncome(int ID) 
        {
            return service.CalculateGrossIncome(ID);
        }

        [HttpGet("~/Employee/Income-After-Tax")]
        public double GetIncomeAfterTax(int ID)
        {
            return service.CalculatIncomeAfterTax(ID);
        }
    } 
}
 