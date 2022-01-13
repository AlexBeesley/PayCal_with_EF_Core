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
        private readonly IRepository<PermEmployeeData> _perm;

        public PermEmployeeController(IRepository<PermEmployeeData> perm)
        {
            _perm = perm;
        }

        [HttpGet()]
        public IActionResult GetAllPermEmployees()
        {
            return Ok(_perm.ReadAll());
        }

        [HttpGet("{ID}")]
        public IActionResult GetPermEmployeeByID(int ID)
        {
            var read = _perm.Read(ID);
            if (read != null)
            {
                return Ok(read);
            }
            else { return NotFound(); }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? Salary, int? Bonus)
        {
            return Ok(_perm.Update(ID, fname, lname, Salary, Bonus));
        }

        [HttpPost()]
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            return Ok(_perm.Create(fname, lname, salary, bonus));
        }

        [HttpDelete("{ID}")]
        public IActionResult DeletePermEmployee(int ID)
        {
            var delete = _perm.Delete(ID);
            if (delete)
            {
                return Ok(delete);
            }
            else { return BadRequest(); }
        }
    }
}
