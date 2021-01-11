using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class MagazzinoUnitaMisure
    {
        public MagazzinoUnitaMisure()
        {
            Magazzinos = new HashSet<Magazzino>();
        }

        public int IdUnitaMisura { get; set; }
        public string UnitaMisura { get; set; }
        public string DescrizioneUnitaMisura { get; set; }

        public virtual ICollection<Magazzino> Magazzinos { get; set; }
    }
}
