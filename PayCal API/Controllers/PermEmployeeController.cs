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
    [Route("~/Permanent-Employees")]
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
            if (response == null) {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http204}\n{LogStrings.context204}");
                return NoContent();
            }
            else {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok(response);
            }
        }

        [HttpGet("{ID}")]
        public IActionResult GetPermEmployeeByID(int ID)
        {
            var read = _perm.Read(ID);
            double pay = _cal.CalculateEmployeePay(ID);
            var output = Json(pay, read);
            if (read != null) {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok(output);
            }
            else {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return NotFound();
            }
        }

        [HttpPut("{ID}")]
        public IActionResult UpdatePermEmployee(int ID, string fname, string lname, int? salary, int? bonus)
        {
            var response =  _perm.Update(ID, fname, lname, salary, bonus);
            if (response == null) {
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
        public IActionResult DeletePermEmployee(int ID)
        {
            var delete = _perm.Delete(ID);
            if (delete) {
                _log.Info($"\nDELETE: {LogStrings.defaultmsg} {LogStrings.http200}");
                return Ok();
            }
            else {
                _log.Warn($"\nDELETE: {LogStrings.errormsg}\n{LogStrings.defaultmsg} {LogStrings.http400}\n{LogStrings.context400}");
                return BadRequest();
            }
        }
    }
}