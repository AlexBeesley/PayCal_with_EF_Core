using Microsoft.AspNetCore.Mvc;
using PayCal;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Temporary")]
    public class TempEmployeeController : Controller
    {
        private TempEmployeeRepository temp;
        private Calculator cal;

        public TempEmployeeController([FromServices] IRepository<TempEmployeeData> Temp)
        {
            temp = (TempEmployeeRepository)Temp;
            cal = new Calculator(null, temp);
        }

        [HttpGet("Employees")]
        public IActionResult GetTempEmployees()
        {
            return Ok($"{(string.Concat(temp.ReadAll()))}");
        }

        [HttpGet("Employee/{ID}")]
        public IActionResult GetTempEmployeeByID(int ID)
        {
            try
            {
                return Ok(string.Concat(temp.Read(ID)));
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Employment-Type")]
        public IActionResult GetTempEmploymentType(int ID)
        {
            try
            {
                return Ok($"Employee with ID: {temp.Read(ID).EmployeeID} is Temporary");
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Full-Name")]
        public IActionResult GetTempEmployeeFullName(int ID)
        {
            try
            {
                return Ok($"{temp.Read(ID).FName} {temp.Read(ID).LName}");
            }            
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Day-Rate")]
        public IActionResult GetTempDayRate(int ID)
        {
            try
            {
                return Ok(temp.Read(ID).DayRateint);
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/WeeksWorked")]
        public IActionResult GetTempWeeksWorked(int ID)
        {
            try
            {
                return Ok(temp.Read(ID).WeeksWorkedint);
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Gross-Income")]
        public IActionResult GetTempGrossIncome(int ID)
        {
            try
            {
                return Ok(cal.CalculateEmployeePay(ID).Item1);
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Income-After-Tax")]
        public IActionResult GetTempIncomeAfterTax(int ID)
        {
            try
            {
                return Ok(cal.CalculateEmployeePay(ID).Item2);
            }
            catch { return NotFound(); }
        }

        [HttpPost("New-Employee")]
        public IActionResult PutNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            try
            {
                return Ok(temp.Create(fname, lname, dayrate, weeksworked));
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete-Employee/{ID}")]
        public IActionResult DeleteTempEmployee(int ID)
        {
            try
            {
                return Ok(temp.Delete(ID));
            }
            catch { return BadRequest(); }
        }
    }
}
