using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class Magazzino
    {
        public int IdMagazzino { get; set; }
        public string NomeArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public string CodiceBarre { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public int IdIva { get; set; }
        public double Quantita { get; set; }
        public double QuantitaMin { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }
        public bool FlagScarico { get; set; }
        public string IdArticolo { get; set; }
        public int IdCategoria { get; set; }
        public int IdUnitaMisura { get; set; }
        public int IdFornitore { get; set; }
        public int IdMarca { get; set; }

        public virtual MagazzinoCategorie IdCategoriaNavigation { get; set; }
        public virtual MagazzinoIva IdIvaNavigation { get; set; }
        public virtual MagazzinoUnitaMisure IdUnitaMisuraNavigation { get; set; }
    }
}
