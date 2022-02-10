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
                ViewData["TempEmployees"] = String.Concat(response);
                return View();
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
                ViewData["TempEmployee"] = response;
                return View();
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
                ViewData["TempPayCalDetails"] = response;
                ViewData["TempPayCal"] = _cal.CalculateEmployeePay(id);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(string fname, string lname, int dayrate, int weeksworked)
        {
            ViewData["TempCreated"] = _temp.Create(fname, lname, dayrate, weeksworked);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, string fname, string lname, int dayrate, int weeksworked)
        {
            ViewData["TempUpdated"] = _temp.Update(id, fname, lname, dayrate, weeksworked);
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var delete = _temp.Delete(id);
            if (delete)
            {
                _log.Info($"\nDELETE: {LogStrings.defaultmsg} {LogStrings.http200}");
                ViewData["TempDeletedid"] = id;
                ViewData["TempDeleted"] = _temp.Delete(id);
                return View();
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