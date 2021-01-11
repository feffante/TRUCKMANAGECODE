using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ManutenzioneUnitaMisuraUsura
    {
        public ManutenzioneUnitaMisuraUsura()
        {
            Manutenzionis = new HashSet<Manutenzioni>();
        }

        public int IdUmUsura { get; set; }
        public string UmUsura { get; set; }
        public string Descrizione { get; set; }

        public virtual ICollection<Manutenzioni> Manutenzionis { get; set; }
    }
}
