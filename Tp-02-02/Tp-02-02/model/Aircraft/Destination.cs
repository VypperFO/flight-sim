using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_02_02.model.Aircrafts
{
    public class Destination
    {
        public Airport AirportDestination;
        public int NbClients;
        public Queue<Aircraft> AircraftQueue;
    }
}
