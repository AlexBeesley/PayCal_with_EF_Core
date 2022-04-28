using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;
using PayCal.Logging;
using log4net;
using System.Reflection;

namespace PayCal_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TempEmployeeController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository<TempEmployeeData> _temp;
        private readonly ICalculator _cal;

        public TempEmployeeController(IRepository<TempEmployeeData> temp, ICalculator cal)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _temp = temp;
            _cal = cal;
        }

        [HttpGet()]
        public IActionResult GetTempEmployees()
        {
            var response = _temp.ReadAll();
            if (response is null) {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http204}\n{LogStrings.context204}");
                return NoContent();
            }
            else {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok(response);
            }
        }

        [HttpGet("{ID}")]
        public IActionResult GetTempEmployeeByID(string ID)
        {
            var read = _temp.Read(ID);
            if (read != null)
            {
                double pay = _cal.CalculateEmployeePay(read.EmployeeID);
                var output = Json(pay, read);
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok(output);
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return NotFound();
            }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdateTempEmployee(string ID, string? fname, string? lname, int? dayrate, int? weeksworked)
        {
            var read = _temp.Read(ID);
            if (fname is null) { fname = read.FName; }
            if (lname is null) { lname = read.LName; }
            if (dayrate is null) { dayrate = read.DayRateint; }
            if (weeksworked is null) { weeksworked = read.WeeksWorkedint; }

            int notnulldayrate = dayrate ?? read.DayRateint;
            int notnullweeksworked = weeksworked ?? read.WeeksWorkedint;

            var response = _temp.Update(ID, fname, lname, notnulldayrate, notnullweeksworked);
            if (response is null) {
                _log.Warn($"\nPUT: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return NotFound();
            }
            else {
                _log.Info($"\nPUT: {LogStrings.defaultmsg} {LogStrings.http204}\n{LogStrings.context204}");
                return NoContent();
            }
        }

        [HttpPost()]
        public IActionResult PostNewTempEmployee(string fname, string lname, int dayrate, int weeksworked)
        {
            var response = _temp.Create(fname, lname, dayrate, weeksworked);
            string uri = ($"{response.EmployeeID}");
            _log.Info($"\nPOST: {LogStrings.defaultmsg} {LogStrings.http201}\n{LogStrings.context201}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeleteTempEmployee(string ID)
        {
            var delete = _temp.Delete(ID);
            if (delete) {
                _log.Info($"\nDELETE: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok();
            }
            else {
                _log.Warn($"\nDELETE: {LogStrings.errormsg}\n{LogStrings.defaultmsg} {LogStrings.http400}\n{LogStrings.context400}");
                return NotFound();
            }
        }
    }
}