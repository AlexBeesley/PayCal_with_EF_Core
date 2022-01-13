using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Temporary")]
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
            return Ok(_temp.ReadAll());
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
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? DayRate, int? WeeksWorked)
        {
            return Ok(_temp.Update(ID, fname, lname, DayRate, WeeksWorked));
        }

        [HttpPost()]
        public IActionResult PutNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            return Ok(_temp.Create(fname, lname, dayrate, weeksworked));
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
