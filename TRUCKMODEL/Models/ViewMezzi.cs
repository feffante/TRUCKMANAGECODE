using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ViewMezzi
    {
        public string TipoMezzo { get; set; }
        public string Modello { get; set; }
        public int Cv { get; set; }
        public DateTime DataImm { get; set; }
        public string Targha { get; set; }
        public string Telaio { get; set; }
        public string Allestimento { get; set; }
        public string Note { get; set; }
        public string Marca { get; set; }
    }
}
