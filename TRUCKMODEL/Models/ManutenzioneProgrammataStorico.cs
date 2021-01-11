using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ManutenzioneProgrammataStorico
    {
        public int IdManutenzioneProgrammata { get; set; }
        public int IdTipoManutenzioneProgrammata { get; set; }
        public int IdMezzo { get; set; }
        public long KmGiornalieri { get; set; }
        public long KmDeltaProssimoIntervento { get; set; }
        public long KmPresunti { get; set; }
        public long KmReali { get; set; }
        public DateTime DtInserimento { get; set; }
        public DateTime DtPrevista { get; set; }
        public int GiorniPreavviso { get; set; }
        public bool ManutenzioneChiusa { get; set; }
    }
}
