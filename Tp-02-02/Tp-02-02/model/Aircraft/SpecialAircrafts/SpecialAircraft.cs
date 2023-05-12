using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.model.Client.SpecialClient;

namespace Tp_02_02.model.Aircraft.SpecialAircrafts
{
    public class SpecialAircraft : Aircraft
    {
        protected Airport DepartureAirport;
        protected SpecialClient Destination;
    }
}
