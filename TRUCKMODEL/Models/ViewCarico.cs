using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ViewCarico
    {
        public string Nota { get; set; }
        public int IdMagazzino { get; set; }
        public int IdCarico { get; set; }
        public DateTime? DtCarico { get; set; }
        public int IdUser { get; set; }
        public string NomeArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public string NomeImpiegato { get; set; }
        public string CognomeImpiegato { get; set; }
    }
}
