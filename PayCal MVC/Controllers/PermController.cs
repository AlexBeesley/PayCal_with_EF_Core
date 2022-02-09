using Microsoft.AspNetCore.Mvc;
using PayCal_MVC.Models;
using System.Diagnostics;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_MVC.Controllers
{
    public class PermController : Controller
    {
        private readonly IRepository<PermEmployeeData> _perm;
        private readonly ICalculator _cal;

        public PermController(IRepository<PermEmployeeData> perm, ICalculator cal)
        {
            _perm = perm;
            _cal = cal;
        }

        public IActionResult Employees()
        {
            ViewData["PermEmployees"] = String.Concat(_perm.ReadAll());
            return View();
        }

        public IActionResult Employee(int id)
        {
            ViewData["PermEmployee"] = _perm.Read(id);
            return View();
        }

        public IActionResult PayCal(int id)
        {
            ViewData["PermPayCalDetails"] = _perm.Read(id);
            ViewData["PermPayCal"] = _cal.CalculateEmployeePay(id);
            return View();
        }

        [HttpPost]
        public IActionResult Create(string fname, string lname, int salary, int bonus)
        {
            ViewData["PermCreated"] = _perm.Create(fname, lname, salary, bonus);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, string fname, string lname, int salary, int bonus)
        {
            ViewData["PermUpdated"] = _perm.Update(id, fname, lname, salary, bonus);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}