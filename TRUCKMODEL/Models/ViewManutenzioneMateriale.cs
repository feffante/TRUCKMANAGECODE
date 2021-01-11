using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ViewManutenzioneMateriale
    {
        public int Iva { get; set; }
        public int IdIva { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public string IdArticolo { get; set; }
        public int IdMagazzinoManutenzione { get; set; }
        public int Quantita { get; set; }
        public int IdManutenzione { get; set; }
        public string CodiceBarre { get; set; }
        public string CodiceArticolo { get; set; }
        public string NomeArticolo { get; set; }
    }
}
