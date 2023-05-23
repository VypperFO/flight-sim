using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.model.Clients.SpecialClients;

namespace Tp_02_02.model.Aircrafts.SpecialAircrafts
{
    public class SpecialAircraft : Aircraft
    {
        protected Airport DepartureAirport;
        protected SpecialClient Destination;
    }
}
