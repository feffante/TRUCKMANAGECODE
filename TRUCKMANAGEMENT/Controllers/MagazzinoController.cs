using Microsoft.AspNetCore.Mvc;

namespace TRUCKMANAGEMENT.Controllers
{
    public class MagazzinoController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}