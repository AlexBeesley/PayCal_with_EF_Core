using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using log4net;
using System.Reflection;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Permanent-Employees")]
    public class PermEmployeeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly LogStrings logStr = new LogStrings();

        private readonly IRepository<PermEmployeeData> _perm;

        public PermEmployeeController(IRepository<PermEmployeeData> perm)
        {
            _perm = perm;
        }

        [HttpGet()]
        public IActionResult GetAllPermEmployees()
        {
            var response = _perm.ReadAll();
            if (response == null) {
                log.Warn($"\nGET: {logStr.defaultmsg} {logStr.http204}\n{logStr.context204}");
                return NoContent();
            }
            else {
                log.Info($"\nGET: {logStr.defaultmsg} {logStr.http200}");
                return Ok(response);
            }
        }

        [HttpGet("{ID}")]
        public IActionResult GetPermEmployeeByID(int ID)
        {
            var read = _perm.Read(ID);
            if (read != null) {
                log.Warn($"\nGET: {logStr.defaultmsg} {logStr.http200}");
                return Ok(read);
            }
            else {
                log.Info($"\nGET: {logStr.defaultmsg} {logStr.http404}\n{logStr.context404}");
                return NotFound();
            }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? salary, int? bonus)
        {
            var response =  _perm.Update(ID, fname, lname, salary, bonus);
            if (response == null) {
                log.Warn($"\nPUT: {logStr.defaultmsg} {logStr.http404}\n{logStr.context404}");
                return NotFound();
            }
            else {
                log.Info($"\nPUT: {logStr.defaultmsg} {logStr.http204}\n{logStr.context204}");
                return NoContent();
            }
        }

        [HttpPost()]
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            var response = _perm.Create(fname, lname, salary, bonus);
            string uri = ($"{response.EmployeeID}");
            log.Info($"\nPOST: {logStr.defaultmsg} {logStr.http201}\n{logStr.context201}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeletePermEmployee(int ID)
        {
            var delete = _perm.Delete(ID);
            if (delete) {
                log.Info($"\nDELETE: {logStr.defaultmsg} {logStr.http200}");
                return Ok(delete);
            }
            else {
                log.Error($"\nDELETE: {logStr.errormsg}\n{logStr.defaultmsg} {logStr.http400}\n{logStr.context400}");
                return BadRequest();
            }
        }
    }
}