using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ViewManutenzioneProgrammata
    {
        public int IdManutenzioneProgrammata { get; set; }
        public int IdMezzo { get; set; }
        public string Modello { get; set; }
        public string TipoManutenzioneProgrammata { get; set; }
        public int IdTipoManutenzioneProgrammata { get; set; }
        public long KmGiornalieri { get; set; }
        public long KmDeltaProssimoIntervento { get; set; }
        public long KmPresunti { get; set; }
        public long KmReali { get; set; }
        public DateTime DtInserimento { get; set; }
        public DateTime DtPrevista { get; set; }
        public int GiorniPreavviso { get; set; }
        public bool ManutenzioneChiusa { get; set; }
        public string Targha { get; set; }
        public DateTime DataImm { get; set; }
        public string Allestimento { get; set; }
        public int IdMarca { get; set; }
        public string Marca { get; set; }
    }
}
