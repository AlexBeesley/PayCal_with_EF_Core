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
    public class PermController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository<PermEmployeeData> _perm;
        private readonly ICalculator _cal;

        public PermController(IRepository<PermEmployeeData> perm, ICalculator cal)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _perm = perm;
            _cal = cal;
        }

        public IActionResult Employees()
        {
            var response = _perm.ReadAll();
            if (response is null)
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http204}\n{LogStrings.context204}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new PermViewModel
                {
                    Employees = response.ToList()
                });
            }
        }

        public IActionResult Employee(string id)
        {
            var response = _perm.Read(id);
            if (response is null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new PermViewModel
                {
                    Employee = response
                });
            }
        }

        public IActionResult PayCal(string id)
        {
            var response = _perm.Read(id);
            if (response is null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new PermViewModel
                {
                    PayCalDetails = _perm.Read(id),
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
        public IActionResult Create(string fname, string lname, int salary, int bonus)
        {
            return View(new PermViewModel
            {
                Created = _perm.Create(fname, lname, salary, bonus)
            });
        }

        [HttpGet("id_editPerm")]
        public IActionResult EditForm(string id)
        {
            var response = _perm.Read(id);
            if (response is null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return RedirectToAction("Index", "ErrorController");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new PermViewModel
                {
                    Employee = response
                });
            }
        }

        [HttpPost]
        public IActionResult Edit(string id, string? fname, string? lname, int? salary, int? bonus)
        {
            var read = _perm.Read(id);
            if (fname is null) { fname = read.FName; }
            if (lname is null) { lname = read.LName; }
            if (salary is null) { salary = read.Salaryint; }
            if (bonus is null) { bonus = read.Bonusint; }

            int notnullsalary = salary ?? read.Salaryint;
            int notnullbonus = bonus ?? read.Bonusint;

            return View(new PermViewModel
            {
                Updated = _perm.Update(id, fname, lname, notnullsalary, notnullbonus)
            });
        }

        [HttpGet("id_deletePerm"), ActionName("Delete")]
        public IActionResult Delete(string id)
        {
            var read = _perm.Read(id);
            var delete = _perm.Delete(id);

            if (delete)
            {
                _log.Info($"\nDELETE: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new PermViewModel
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
            if (searchString is null)
            {
                return RedirectToAction("Employees");
            }

            IEnumerable<PermEmployeeData> permEmployeesSearched = _perm.ReadAll().Where(s => s.FName.Contains(searchString) || s.LName.Contains(searchString));

            return View("Employees", new PermViewModel
            {
                Employees = permEmployeesSearched.ToList()
            });
        }
    }
}