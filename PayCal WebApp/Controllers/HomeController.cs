using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<PermEmployeeData> _perm;
        private readonly ICalculator _cal;

        public HomeController(IRepository<PermEmployeeData> perm, ICalculator cal)
        {
            _perm = perm;
            _cal = cal;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var response = _perm.ReadAll();
            if (response == null)
            {
                return NoContent();
            }
            else
            {
                return View(response);
            }
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
