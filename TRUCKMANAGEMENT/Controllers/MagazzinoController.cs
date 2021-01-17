using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TRUCKMANAGEMENT.Models.Services.Application;
using TRUCKMANAGEMENT.Models.ViewModels;

namespace TRUCKMANAGEMENT.Controllers
{
    public class MagazzinoController : Controller
    {
        private readonly MagazzinoService magazzinoService;
        public MagazzinoController(MagazzinoService magazzinoService)
        {
            this.magazzinoService = magazzinoService;

        }
        public IActionResult Index()
        {
            //var magazzinoService = new MagazzinoService();
            List<MagazzinoViewModel> magazzino = magazzinoService.GetMagazzinoLista();

            return View(magazzino);
        }

        public IActionResult Detail(string id)
        {
            //var magazzinoService = new MagazzinoService();
            MagazzinoDettaglioViewModel ViewModel = magazzinoService.GetMagazzinoDettaglio(id);

            return View(ViewModel);
        }
    }
}