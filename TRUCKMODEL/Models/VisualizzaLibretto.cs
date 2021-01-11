using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class VisualizzaLibretto
    {
        public int IdVisualizzaLibretto { get; set; }
        public int IdMezzo { get; set; }
        public string Path { get; set; }
        public string Note { get; set; }
    }
}
