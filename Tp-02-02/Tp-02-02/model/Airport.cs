using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.model.Aircraft;

namespace Tp_02_02.model
{
    public class Airport
    {
        public string Name { get; set; }
        public Position Coords { get; set; }
        public int MinPassenger { get; set; }
        public int MaxPassenger { get; set; }
        public int MinMerchandise{ get; set; }
        public int MaxMerchandise { get; set; }
        public List<Airport> Airports { get; set; }
        public List<Destination> DestinationsList { get; set; }
    }
}
