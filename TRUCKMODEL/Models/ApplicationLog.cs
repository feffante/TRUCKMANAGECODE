using System;
using System.Collections.Generic;

#nullable disable

namespace TRUCKMODEL.Models
{
    public partial class ApplicationLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Severity { get; set; }
        public string Message { get; set; }
    }
}
