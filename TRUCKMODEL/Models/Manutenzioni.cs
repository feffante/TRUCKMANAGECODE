using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class Manutenzioni
    {
        public int IdManutenzione { get; set; }
        public DateTime? DtInizio { get; set; }
        public DateTime? DtFine { get; set; }
        public string Descrizione { get; set; }
        public decimal PrezzoTot { get; set; }
        public int UmValore { get; set; }
        public string Note { get; set; }
        public int IdManutenzioneTipo { get; set; }
        public int IdMezzo { get; set; }
        public int IdUnitaMisura { get; set; }

        public virtual ManutenzioneTipo IdManutenzioneTipoNavigation { get; set; }
        public virtual AnagraficaMezzi IdMezzoNavigation { get; set; }
        public virtual ManutenzioneUnitaMisuraUsura IdUnitaMisuraNavigation { get; set; }
    }
}
