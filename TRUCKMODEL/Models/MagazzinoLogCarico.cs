using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class MagazzinoLogCarico
    {
        public int IdCarico { get; set; }
        public string Nota { get; set; }
        public DateTime? DtCarico { get; set; }
        public int IdMagazzino { get; set; }
        public int IdUser { get; set; }
    }
}
