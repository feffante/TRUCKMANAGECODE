using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ManutenzioneMagazzino
    {
        public int IdMagazzinoManutenzione { get; set; }
        public int Quantita { get; set; }
        public int IdMagazzino { get; set; }
        public int IdManutenzione { get; set; }
    }
}
