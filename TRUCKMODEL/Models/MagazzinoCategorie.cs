using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class MagazzinoCategorie
    {
        public MagazzinoCategorie()
        {
            Magazzinos = new HashSet<Magazzino>();
        }

        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string DescrizioneCategoria { get; set; }

        public virtual ICollection<Magazzino> Magazzinos { get; set; }
    }
}
