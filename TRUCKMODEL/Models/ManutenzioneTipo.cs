using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ManutenzioneTipo
    {
        public ManutenzioneTipo()
        {
            Manutenzionis = new HashSet<Manutenzioni>();
        }

        public int IdTipoManutenzione { get; set; }
        public string TipoManutenzione { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Manutenzioni> Manutenzionis { get; set; }
    }
}
