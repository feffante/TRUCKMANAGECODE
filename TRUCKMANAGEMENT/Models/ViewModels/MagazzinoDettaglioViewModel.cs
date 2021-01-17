using System.Linq;
using TRUCKMODEL.Models;

namespace TRUCKMANAGEMENT.Models.ViewModels
{
    public class MagazzinoDettaglioViewModel
    {

          public string NomeArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public string CodiceBarre { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public double Quantita { get; set; }
        public double QuantitaMin { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }
        public bool FlagScarico { get; set; }
        public int IdMagazzino { get; set; }
        public int IdIva { get; set; }
        public int Iva { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdUnitaMisura { get; set; }
        public string UnitaMisura { get; set; }
        public int CodiceFornitore { get; set; }
        public int IdFornitore { get; set; }
        public string NomeFornitore { get; set; }
        public string IdArticolo { get; set; }
        public string Marca { get; set; }
        public int IdMarca { get; set; }
        
    }
}