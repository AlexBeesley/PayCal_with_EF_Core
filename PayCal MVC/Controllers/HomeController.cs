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
    public class HomeController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository<TempEmployeeData> _temp;
        private readonly IRepository<PermEmployeeData> _perm;

        public HomeController(IRepository<TempEmployeeData> temp,IRepository<PermEmployeeData> perm)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _temp = temp;
            _perm = perm;
        }

        public IActionResult Index()
        {
            ViewData["Count"] = _temp.Count() + _perm.Count();
            ViewData["tempList"] = String.Concat(_temp.ReadAll());
            ViewData["permList"] = String.Concat(_perm.ReadAll());
            _log.Info($"\nGET: {LogStrings.defaultmsg} {LogStrings.http200}");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}