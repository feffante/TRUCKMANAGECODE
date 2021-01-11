using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class AnagraficaImpiegati
    {
        public int IdImpiegato { get; set; }
        public string NomeImpiegato { get; set; }
        public string CognomeImpiegato { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }
        public string NumTel { get; set; }
        public string Cellulare { get; set; }
        public string Email { get; set; }
        public string Cfisc { get; set; }
        public DateTime DtInserimento { get; set; }
    }
}
