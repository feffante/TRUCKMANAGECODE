using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class MagazzinoLogScarico
    {
        public int IdScarico { get; set; }
        public string Nota { get; set; }
        public DateTime? DtScarico { get; set; }
        public int IdMagazzino { get; set; }
        public int IdUser { get; set; }
    }
}
