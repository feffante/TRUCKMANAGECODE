using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ViewManutenzioni
    {
        public DateTime? DtInizio { get; set; }
        public DateTime? DtFine { get; set; }
        public int IdManutenzione { get; set; }
        public string Descrizione { get; set; }
        public decimal PrezzoTot { get; set; }
        public int UmValore { get; set; }
        public string Note { get; set; }
        public int IdUmUsura { get; set; }
        public string UmUsura { get; set; }
        public string TipoManutenzione { get; set; }
        public int IdManutenzioneTipo { get; set; }
        public int IdUnitaMisura { get; set; }
        public int IdTipoMezzo { get; set; }
        public int IdMezzo { get; set; }
        public string Modello { get; set; }
        public int Cv { get; set; }
        public string Targha { get; set; }
        public DateTime DataImm { get; set; }
        public string Telaio { get; set; }
        public string Allestimento { get; set; }
        public string NoteMezzi { get; set; }
        public int IdMarca { get; set; }
    }
}
