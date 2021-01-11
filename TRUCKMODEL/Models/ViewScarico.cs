using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ViewScarico
    {
        public string NomeArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public int IdUser { get; set; }
        public int IdMagazzino { get; set; }
        public DateTime? DtScarico { get; set; }
        public string Nota { get; set; }
        public int IdScarico { get; set; }
        public string NomeImpiegato { get; set; }
        public string CognomeImpiegato { get; set; }
    }
}
