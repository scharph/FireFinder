using System;
namespace FireFinder.Logic.Models
{
    public class Destination
    {
        public string Title { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string Addon { get; set; }
        public District District { get; set; }
        public Geo Geo { get; set; }
    }
}
