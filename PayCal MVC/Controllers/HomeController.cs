using Microsoft.AspNetCore.Mvc;
using PayCal_MVC.Models;
using System.Diagnostics;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<TempEmployeeData> _temp;
        private readonly IRepository<PermEmployeeData> _perm;
        private readonly ICalculator _cal;

        public HomeController(IRepository<TempEmployeeData> temp,IRepository<PermEmployeeData> perm, ICalculator cal)
        {
            _temp = temp;
            _perm = perm;
            _cal = cal;
        }

        public IActionResult Index()
        {
            ViewData["Count"] = _temp.Count() + _perm.Count();
            ViewData["tempList"] = String.Concat(_temp.ReadAll());
            ViewData["permList"] = String.Concat(_perm.ReadAll());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}