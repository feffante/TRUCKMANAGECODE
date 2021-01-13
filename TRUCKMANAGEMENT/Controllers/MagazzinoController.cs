using Microsoft.AspNetCore.Mvc;

namespace TRUCKMANAGEMENT.Controllers
{
    public class MagazzinoController : Controller
    {
        public IActionResult Index()
        {
            return Content("sono index");
        }
        public IActionResult Detail(string id)
        {
            return Content($"sono Detail, ho ricevuto l'id = {id}");
        }
    }
}