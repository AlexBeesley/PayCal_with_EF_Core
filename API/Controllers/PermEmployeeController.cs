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
    public class PermEmployeeController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository<PermEmployeeData> _perm;
        private readonly ICalculator _cal;

        public PermEmployeeController(IRepository<PermEmployeeData> perm, ICalculator cal)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _perm = perm;
            _cal = cal;
        }

        [HttpGet()]
        public IActionResult GetAllPermEmployees()
        {
            var response = _perm.ReadAll();
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
        public IActionResult GetPermEmployeeByID(string ID)
        {
            var read = _perm.Read(ID);
            if (read != null)
            {
                double? pay = _cal.CalculateEmployeePay(read.EmployeeID);
                var output = Json(pay, read);
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok(output);
            }
            else
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return NotFound();
            }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdatePermEmployee(string ID, string? fname, string? lname, int? salary, int? bonus)
        {
            var read = _perm.Read(ID);
            if (fname is null) { fname = read.FName;  }
            if (lname is null) { lname = read.LName; }
            if (salary is null) { salary = read.Salaryint;  }
            if (bonus is null) { bonus = read.Bonusint; }

            int notnullsalary = salary ?? read.Salaryint;
            int notnullbonus = bonus ?? read.Bonusint;

            var response =  _perm.Update(ID, fname, lname, notnullsalary, notnullbonus);
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
        public IActionResult PostNewPermEmployee(string fname, string lname, int salary, int bonus)
        {
            var response = _perm.Create(fname, lname, salary, bonus);
            string uri = ($"{response.EmployeeID}");
            _log.Info($"\nPOST: {LogStrings.defaultmsg} {LogStrings.http201}\n{LogStrings.context201}");
            return Created(uri, response);
        }

        [HttpDelete("{ID}")]
        public IActionResult DeletePermEmployee(string ID)
        {
            var delete = _perm.Delete(ID);
            if (delete)
            {
                _log.Info($"\nDELETE: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok();
            }
            else
            {
                _log.Warn($"\nDELETE: {LogStrings.errormsg}\n{LogStrings.defaultmsg} {LogStrings.http400}\n{LogStrings.context400}");
                return NotFound();
            }
        }
    }
}