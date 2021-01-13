using Microsoft.AspNetCore.Mvc;

namespace TRUCKMANAGEMENT.Controllers
{
    public class MagazzinoController : Controller
    {
        public IActionResult Index()
        { 
            return Content("sono iundex di magazzino controller");
        }

        public IActionResult Detail(string id)
        {
            return Content($"sono detaildi magazzino ho ricevuto id: {id}");
        }
    }
}