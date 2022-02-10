using Microsoft.AspNetCore.Mvc;
using PayCal_MVC.Models;
using System.Diagnostics;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_MVC.Controllers
{
    public class TempController : Controller
    {
        private readonly IRepository<TempEmployeeData> _temp;
        private readonly ICalculator _cal;

        public TempController(IRepository<TempEmployeeData> temp, ICalculator cal)
        {
            _temp = temp;
            _cal = cal;
        }

        public IActionResult Employees()
        {
            ViewData["TempEmployees"] = String.Concat(_temp.ReadAll());
            return View();
        }

        public IActionResult Employee(int id)
        {
            ViewData["TempEmployee"] = _temp.Read(id);
            return View();
        }

        public IActionResult PayCal(int id)
        {
            ViewData["TempPayCalDetails"] = _temp.Read(id);
            ViewData["TempPayCal"] = _cal.CalculateEmployeePay(id);
            return View();
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            ViewData["TempDeletedid"] = id;
            ViewData["TempDeleted"] = _temp.Delete(id);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}