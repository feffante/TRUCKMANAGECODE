using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TRUCKMANAGEMENT.Models.Services.Application;
using TRUCKMANAGEMENT.Models.ViewModels;

namespace TRUCKMANAGEMENT.Controllers
{
    public class MagazzinoController : Controller
    {
        public IActionResult Index()
        { 
            var magazzinoService = new MagazzinoService();
            List<MagazzinoViewModel> magazzino = magazzinoService.GetMagazzino();

            return View(magazzino);
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}