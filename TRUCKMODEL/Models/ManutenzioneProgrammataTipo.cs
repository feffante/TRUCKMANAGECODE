using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ManutenzioneProgrammataTipo
    {
        public ManutenzioneProgrammataTipo()
        {
            ManutenzioneProgrammata = new HashSet<ManutenzioneProgrammata>();
        }

        public int IdTipoManutenzioneProgrammata { get; set; }
        public string TipoManutenzioneProgrammata { get; set; }

        public virtual ICollection<ManutenzioneProgrammata> ManutenzioneProgrammata { get; set; }
    }
}
