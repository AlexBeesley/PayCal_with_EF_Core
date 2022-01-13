using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Permanent")]
    public class PermEmployeeController : Controller
    {
        private readonly IRepository<PermEmployeeData> perm;

        public PermEmployeeController(IRepository<PermEmployeeData> Perm)
        {
            perm = Perm;
        }

        [HttpGet()]
        public IActionResult GetAllPermEmployees()
        {
            return Ok(perm.ReadAll());
        }

        [HttpGet("{ID}")]
        public IActionResult GetPermEmployeeByID(int ID)
        {
            var read = perm.Read(ID);
            if (read != null)
            {
                return Ok(read);
            }
            else { return NotFound(); }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? Salary, int? Bonus)
        {
            return Ok(perm.Update(ID, fname, lname, Salary, Bonus));
        }

        [HttpPost()]
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            return Ok(perm.Create(fname, lname, salary, bonus));
        }

        [HttpDelete("{ID}")]
        public IActionResult DeletePermEmployee(int ID)
        {
            var delete = perm.Delete(ID);
            if (delete)
            {
                return Ok(delete);
            }
            else { return BadRequest(); }
        }
    }
}
