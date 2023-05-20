using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_02_02.model
{
    public class Scenario
    {
        public List<Airport> AirportList { get; set; }

        public Scenario()
        {
            AirportList = new List<Airport>();
        }
    }
}
