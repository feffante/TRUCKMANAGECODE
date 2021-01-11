using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class AnagraficaFornitori
    {
        public int IdAnagFornitore { get; set; }
        public int CodiceFornitore { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }
        public string NumTel { get; set; }
        public string NumFax { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string Piva { get; set; }
        public string Cfisc { get; set; }
        public string Contatto { get; set; }
        public string Note { get; set; }
    }
}
