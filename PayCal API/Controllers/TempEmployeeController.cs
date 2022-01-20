using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Temporary-Employees")]
    public class TempEmployeeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRepository<TempEmployeeData> _temp;

        public TempEmployeeController(IRepository<TempEmployeeData> temp)
        {
            _temp = temp;
        }

        public string defaultmsg = ("Request returned with HTTP Status code: ");
        public string http200 = ("200 (Ok)");
        public string http201 = ("201 (Created)");
        public string http204 = ("204 (No Content)");
        public string http400 = ("400 (Bad Request)");
        public string http404 = ("404 (Not Found)");

        [HttpGet()]
        public IActionResult GetTempEmployees()
        {
            var response = _temp.ReadAll();
            if (response == null) {
                log.Info($"GET: {defaultmsg} {http204}");
                return NoContent();
            }
            else {
                log.Info($"GET: {defaultmsg} {http200}");
                return Ok(response);
            }
        }

        [HttpGet("{ID}")]
        public IActionResult GetTempEmployeeByID(int ID)
        {
            var read = _temp.Read(ID);
            if (read != null) {
                log.Info($"GET: {defaultmsg} {http200}");
                return Ok(read);
            }
            else {
                log.Info($"GET: {defaultmsg} {http404}");
                return NotFound();
            }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdateTempEmployee(int ID, string fname, string lname, int? dayrate, int? weeksworked)
        {
            var response =_temp.Update(ID, fname, lname, dayrate, weeksworked);
            if (response == null) {
                log.Info($"PUT: {defaultmsg} {http404}");
                return NotFound();
            }
            else {
                log.Info($"PUT: {defaultmsg} {http204}");
                return NoContent();
            }
        }

        [HttpPost()]
        public IActionResult PostNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            var response = _temp.Create(fname, lname, dayrate, weeksworked);
            string uri = ($"{response.EmployeeID}");
            log.Info($"POST: {defaultmsg} {http201}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeleteTempEmployee(int ID)
        {
            var delete = _temp.Delete(ID);
            if (delete) {
                log.Info($"DELETE: {defaultmsg} {http200}");
                return Ok(delete);
            }
            else {
                log.Info($"DELETE: {defaultmsg} {http400}");
                return BadRequest();
            }
        }
    }
}