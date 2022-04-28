using Microsoft.AspNetCore.Mvc;
using PayCal_MVC.Models;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;
using PayCal.Logging;
using PayCal.Extensions;
using log4net;
using System.Reflection;

namespace PayCal_MVC.Controllers
{
    public class TempController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository<TempEmployeeData> _temp;
        private readonly ICalculator _cal;

        public TempController(IRepository<TempEmployeeData> temp, ICalculator cal)
        {
            System.Type? declaringType = MethodBase.GetCurrentMethod().DeclaringType;            
            if (declaringType is null)
            {
                throw new System.ArgumentNullException(nameof(declaringType));
            }
            
            _log = LogManager.GetLogger(declaringType);
            _temp = temp;
            _cal = cal;
        }

        public IActionResult Employees()
        {
            var response = _temp.ReadAll();
            if (response is null)
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http204}\n{LogStrings.context204}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new TempViewModel
                {
                    Employees = response.ToList()
                });
            }
        }

        public IActionResult Employee(string id)
        {
            var response = _temp.Read(id);
            
            if (response is null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");

                return View(new TempViewModel
                {
                    Employee = response
                });
            }
        }

        public IActionResult PayCal(string id)
        {
            var response = _temp.Read(id);
            if (response is null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new TempViewModel
                {
                    PayCalDetails = _temp.Read(id),
                    PayCalculated = _cal.CalculateEmployeePay(id)
                });
            }
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string fname, string lname, int dayrate, int weeksworked)
        {
            return View(new TempViewModel
            {
                Created = _temp.Create(fname, lname, dayrate, weeksworked)
            });
        }

        [HttpGet("id_editTemp")]
        public IActionResult EditForm(string id)
        {
            var response = _temp.Read(id);
            if (response is null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new TempViewModel
                {
                    Employee = response
                });
            }
        }

        [HttpPost]
        public IActionResult Edit(string id, string? fname, string? lname, int? dayrate, int? weeksworked)
        {
            var read = _temp.Read(id);
            if (fname is null) { fname = read.FName; }
            if (lname is null) { lname = read.LName; }
            if (dayrate is null) { dayrate = read.DayRateint; }
            if (weeksworked is null) { weeksworked = read.WeeksWorkedint; }

            int notnulldayrate = dayrate ?? read.DayRateint;
            int notnullweeksworked = weeksworked ?? read.WeeksWorkedint;

            return View(new TempViewModel
            {
                Updated = _temp.Update(id, fname, lname, notnulldayrate, notnullweeksworked)
            });
        }

        [HttpGet("id_deleteTemp"), ActionName("Delete")]
        public IActionResult Delete(string id)
        {
            var read = _temp.Read(id);
            var delete = _temp.Delete(id);

            if (delete)
            {
                _log.Info($"\nDELETE: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new TempViewModel
                {
                    DeletedID = id,
                    Deleted = read
                });
            }
            else
            {
                _log.Warn($"\nDELETE: {LogStrings.errormsg}\n{LogStrings.defaultmsg} {LogStrings.http400}\n{LogStrings.context400}");
                return RedirectToAction("Index", "ErrorController");
            }
        }

        public IActionResult Search(string searchString)
        {
            _log.Info($"\nDEBUG: search string is >>> {searchString} <<<");
            if (searchString is null)
            {
                return RedirectToAction("Employees");
            }

            IEnumerable<TempEmployeeData> tempEmployeesSearched = _temp.ReadAll().Where(s => s.FName.Contains(searchString) || s.LName.Contains(searchString));

            return View("Employees", new TempViewModel
            {
                Employees = tempEmployeesSearched.ToList()
            });
        }
    }
}