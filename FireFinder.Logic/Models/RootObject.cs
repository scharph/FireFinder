using System;
using System.Collections.Generic;

namespace FireFinder.Logic.Models
{
    public class RootObject
    {
        public DateTime LastRefresh { get; set; }
        public DateTime Published { get; set; }
        public string Title { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
