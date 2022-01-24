using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using log4net;
using System.Reflection;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("~/Temporary-Employees")]
    public class TempEmployeeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly LogStrings logStr = new LogStrings();

        private readonly IRepository<TempEmployeeData> _temp;

        public TempEmployeeController(IRepository<TempEmployeeData> temp)
        {
            _temp = temp;
        }

        [HttpGet()]
        public IActionResult GetTempEmployees()
        {
            var response = _temp.ReadAll();
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
        public IActionResult GetTempEmployeeByID(int ID)
        {
            var read = _temp.Read(ID);
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
        public IActionResult UpdateTempEmployee(int ID, string fname, string lname, int? dayrate, int? weeksworked)
        {
            var response =_temp.Update(ID, fname, lname, dayrate, weeksworked);
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
        public IActionResult PostNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            var response = _temp.Create(fname, lname, dayrate, weeksworked);
            string uri = ($"{response.EmployeeID}");
            log.Info($"\nPOST: {logStr.defaultmsg} {logStr.http201}\n{logStr.context201}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeleteTempEmployee(int ID)
        {
            var delete = _temp.Delete(ID);
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