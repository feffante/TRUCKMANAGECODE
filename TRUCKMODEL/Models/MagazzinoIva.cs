using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class MagazzinoIva
    {
        public MagazzinoIva()
        {
            Magazzinos = new HashSet<Magazzino>();
        }

        public int IdIva { get; set; }
        public int Iva { get; set; }
        public string DescrizioneIva { get; set; }

        public virtual ICollection<Magazzino> Magazzinos { get; set; }
    }
}
