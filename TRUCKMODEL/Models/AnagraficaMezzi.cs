using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class AnagraficaMezzi
    {
        public AnagraficaMezzi()
        {
            ManutenzioneProgrammata = new HashSet<ManutenzioneProgrammata>();
            Manutenzionis = new HashSet<Manutenzioni>();
        }

        public int IdMezzo { get; set; }
        public string Modello { get; set; }
        public int Cv { get; set; }
        public DateTime DataImm { get; set; }
        public string Targha { get; set; }
        public string Telaio { get; set; }
        public string Allestimento { get; set; }
        public string Note { get; set; }
        public int IdTipoMezzo { get; set; }
        public int IdMarca { get; set; }

        public virtual ICollection<ManutenzioneProgrammata> ManutenzioneProgrammata { get; set; }
        public virtual ICollection<Manutenzioni> Manutenzionis { get; set; }
    }
}
