using Microsoft.AspNetCore.Mvc;

namespace PayCal_MVC.Controllers
{
    [Route("Error/{statuscode}")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int statuscode)
        {
            switch (statuscode)
            {
                case 404:
                    ViewData["ErrorMsg"] = "404: Page Not Found.";
                    break;
                case 400:
                    ViewData["ErrorMsg"] = "400: Bad Request";
                    break;
                default: 
                    break;
            }
            return View("ErrorPage");
        }
    }
}
