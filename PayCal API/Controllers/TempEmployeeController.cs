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

        [HttpGet()]
        public IActionResult GetTempEmployees()
        {
            return Ok(temp.ReadAll());
        }

        [HttpGet("{ID}")]
        public IActionResult GetTempEmployeeByID(int ID)
        {
            try
            {
                return Ok(temp.Read(ID));
            }
            catch { return NotFound(); }
        }

        //[HttpPut("{ID}")]
        //public IActionResult UpdateTempEmployee(int ID)
        //{
        //    add code
        //}

        [HttpPost()]
        public IActionResult PutNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            try
            {
                return Ok(temp.Create(fname, lname, dayrate, weeksworked));
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("{ID}")]
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
