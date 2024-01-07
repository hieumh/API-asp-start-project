using Microsoft.AspNetCore.Mvc;

namespace API_asp_start_project.API
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
