using Microsoft.AspNetCore.Mvc;
using PayCal;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Permanent")]
    public class PermEmployeeController : Controller
    {
        private PermEmployeeRepository perm;
        private Calculator cal;

        public PermEmployeeController([FromServices] IRepository<PermEmployeeData> Perm)
        {
            perm = (PermEmployeeRepository)Perm;
            cal = new Calculator(perm, null);
        }

        [HttpGet("Employees")]
        public IActionResult GetAllPermEmployees()
        {
            return Ok($"{(string.Concat(perm.ReadAll()))}");
        }

        [HttpGet("Employee/{ID}")]
        public IActionResult GetPermEmployeeByID(int ID)
        {
            try
            {
                return Ok(string.Concat(perm.Read(ID)));
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Employment-Type")]
        public IActionResult GetPermEmploymentType(int ID)
        {
            try
            {
                return Ok($"Employee with ID: {perm.Read(ID).EmployeeID} is Permanent");
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Full-Name")]
        public IActionResult GetPermEmployeeFullName(int ID)
        {
            try
            {
                return Ok($"{perm.Read(ID).FName} {perm.Read(ID).LName}");
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Salary")]
        public IActionResult GetPermSalary(int ID)
        {
            try
            {
                return Ok(perm.Read(ID).Salaryint);
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/WeeksWorked")]
        public IActionResult GetPermBonus(int ID)
        {
            try
            {
                return Ok(perm.Read(ID).Bonusint);
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Gross-Income")]
        public IActionResult GetPermGrossIncome(int ID)
        {
            try
            {
                return Ok(cal.CalculateEmployeePay(ID).Item1);
            }
            catch { return NotFound(); }
        }

        [HttpGet("Employee/{ID}/Income-After-Tax")]
        public IActionResult GetPermIncomeAfterTax(int ID)
        {
            try
            {
                return Ok(cal.CalculateEmployeePay(ID).Item2);
            }
            catch { return NotFound(); }
        }

        [HttpPost("New-Employee")]
        public IActionResult PutNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            try
            {
                return Ok(perm.Create(fname, lname, salary, bonus));
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete-Employee/{ID}")]
        public IActionResult DeletePermEmployee(int ID)
        {
            try
            {
                return Ok(perm.Delete(ID));
            }
            catch { return BadRequest(); }
        }
    }
}
