using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02.model.Aircrafts;

namespace Tp_02.model
{
    public class Airport
    {
        public string Name { get; set; }
        public Position Coords { get; set; }
        public int MinPassenger { get; set; }
        public int MaxPassenger { get; set; }
        public int MinMerchandise { get; set; }
        public int MaxMerchandise { get; set; }
        public List<Aircraft> AircraftList { get; set; }

    }
}
