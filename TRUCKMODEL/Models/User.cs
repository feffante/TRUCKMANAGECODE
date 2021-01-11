using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string User1 { get; set; }
        public string Password { get; set; }
        public DateTime DtCreazione { get; set; }
        public string Note { get; set; }
    }
}
