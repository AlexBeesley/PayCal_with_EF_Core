using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Temporary-Employees")]
    public class TempEmployeeController : Controller
    {
        private readonly IRepository<TempEmployeeData> _temp;

        public TempEmployeeController(IRepository<TempEmployeeData> temp)
        {
            _temp = temp;
        }

        [HttpGet()]
        public IActionResult GetTempEmployees()
        {
            var response = _temp.ReadAll();
            if (response == null) { return NoContent(); }
            else { return Ok(response); }
        }

        [HttpGet("{ID}")]
        public IActionResult GetTempEmployeeByID(int ID)
        {
            var read = _temp.Read(ID);
            if (read != null)
            {
                return Ok(read);
            }
            else { return NotFound(); }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdateTempEmployee(int ID, string fname, string lname, int? dayrate, int? weeksworked)
        {
            var response =_temp.Update(ID, fname, lname, dayrate, weeksworked);
            if (response == null) { return NotFound(); }
            else { return NoContent(); }
        }

        [HttpPost()]
        public IActionResult PostNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            var response = _temp.Create(fname, lname, dayrate, weeksworked);
            string uri = ($"{response.EmployeeID}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeleteTempEmployee(int ID)
        {
            var delete = _temp.Delete(ID);
            if (delete)
            {
                return Ok(delete);
            }
            else { return BadRequest(); }
        }
    }
}
