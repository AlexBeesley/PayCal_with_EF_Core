using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Permanent-Employees")]
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
            var response = _perm.ReadAll();
            if (response == null) { return NoContent(); }
            else { return Ok(response); }
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
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? salary, int? bonus)
        {
            var response =  _perm.Update(ID, fname, lname, salary, bonus);
            if (response == null) { return NotFound(); }
            else { return NoContent(); }
        }

        [HttpPost()]
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            var response = _perm.Create(fname, lname, salary, bonus);
            string uri = ($"{response.EmployeeID}");
            return Created(uri, response);
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
