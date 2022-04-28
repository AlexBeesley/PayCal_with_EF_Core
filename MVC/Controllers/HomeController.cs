using Microsoft.AspNetCore.Mvc;
using PayCal_MVC.Models;
using System.Diagnostics;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Logging;
using log4net;
using System.Reflection;

namespace PayCal_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository<TempEmployeeData> _temp;
        private readonly IRepository<PermEmployeeData> _perm;

        public HomeController(IRepository<TempEmployeeData> temp, IRepository<PermEmployeeData> perm)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _temp = temp;
            _perm = perm;
        }

        public IActionResult Index()
        {
            _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
            return View(new HomeViewModel
            {
                tempCount = _temp.Count(),
                permCount = _perm.Count(),
                tempList = _temp.ReadAll().ToList(),
                permList = _perm.ReadAll().ToList()
            });
        }

        public IActionResult Search(string searchString)
        {
            _log.Info($"\nDEBUG: search string is >>> {searchString} <<<");
            if (searchString is null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<TempEmployeeData> tempEmployeesSearched = _temp.ReadAll().Where(s => s.FName.Contains(searchString) || s.LName.Contains(searchString));
            IEnumerable<PermEmployeeData> permEmployeesSearched = _perm.ReadAll().Where(s => s.FName.Contains(searchString) || s.LName.Contains(searchString));

            return View("Index", new HomeViewModel
            {
                tempCount = tempEmployeesSearched.Count(),
                permCount = permEmployeesSearched.Count(),
                tempList = tempEmployeesSearched.ToList(),
                permList = permEmployeesSearched.ToList()
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}