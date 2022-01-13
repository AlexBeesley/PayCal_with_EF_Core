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

        [HttpGet()]
        public IActionResult GetAllPermEmployees()
        {
            return Ok(perm.ReadAll());
        }

        [HttpGet("{ID}")]
        public IActionResult GetPermEmployeeByID(int ID)
        {
            try
            {
                return Ok(perm.Read(ID));
            }
            catch { return NotFound(); }
        }

        //[HttpPut("{ID}")]
        //public IActionResult UpdatePermEmployee(int ID)
        //{
        //    add code
        //}

        [HttpPost()]
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            try
            {
                return Ok(perm.Create(fname, lname, salary, bonus));
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("{ID}")]
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
