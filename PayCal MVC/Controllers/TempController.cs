using Microsoft.AspNetCore.Mvc;
using PayCal_MVC.Models;
using System.Diagnostics;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;
using PayCal.Logging;
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
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _temp = temp;
            _cal = cal;
        }

        public IActionResult Employees()
        {
            var response = _temp.ReadAll();
            if (response == null)
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http204}\n{LogStrings.context204}");
                return View("NoContent");
            }
            else
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new TempViewModel
                {
                    Employees = String.Concat(response)
                });
            }
        }

        public IActionResult Employee(int id)
        {
            var response = _temp.Read(id);
            if (response == null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return View("NotFound");
            }
            else
            {
                _log.Warn($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
                return View(new TempViewModel
                {
                    Employee = String.Concat(response)
                });
            }
        }

        public IActionResult PayCal(int id)
        {
            var response = _temp.Read(id);
            if (response == null)
            {
                _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http404}\n{LogStrings.context404}");
                return View("NotFound");
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

        [HttpPost]
        public IActionResult Create(string fname, string lname, int dayrate, int weeksworked)
        {
            return View(new TempViewModel
            {
                Created = _temp.Create(fname, lname, dayrate, weeksworked)
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, string fname, string lname, int dayrate, int weeksworked)
        {
            return View(new TempViewModel
            {
                Updated = _temp.Update(id, fname, lname, dayrate, weeksworked)
            });
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
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
                return View("NotFound");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}