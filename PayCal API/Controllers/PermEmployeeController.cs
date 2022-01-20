using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Permanent-Employees")]
    public class PermEmployeeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRepository<PermEmployeeData> _perm;

        public PermEmployeeController(IRepository<PermEmployeeData> perm)
        {
            _perm = perm;
        }

        public string defaultmsg = ("Request returned with HTTP Status code: ");
        public string http200 = ("200 (Ok)");
        public string http201 = ("201 (Created)");
        public string http204 = ("204 (No Content)");
        public string http400 = ("400 (Bad Request)");
        public string http404 = ("404 (Not Found)");

        [HttpGet()]
        public IActionResult GetAllPermEmployees()
        {
            var response = _perm.ReadAll();
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
        public IActionResult GetPermEmployeeByID(int ID)
        {
            var read = _perm.Read(ID);
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
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? salary, int? bonus)
        {
            var response =  _perm.Update(ID, fname, lname, salary, bonus);
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
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            var response = _perm.Create(fname, lname, salary, bonus);
            string uri = ($"{response.EmployeeID}");
            log.Info($"POST: {defaultmsg} {http201}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeletePermEmployee(int ID)
        {
            var delete = _perm.Delete(ID);
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