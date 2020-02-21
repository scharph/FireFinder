using System;
using System.Collections.Generic;

namespace FireFinder.Logic.Models
{
    public class Operation
    {
        public string Id { get; set; }
        public string Operationtype { get; set; }
        public DateTime Starttime { get; set; }
        public Nullable<DateTime> Endtime { get; set; }
        public string State { get; set; }
        public int Alarmlevel { get; set; }
        public Type Type { get; set; }
        public Destination Destination { get; set; }
        public List<Unit> Units { get; set; }
    }
}
