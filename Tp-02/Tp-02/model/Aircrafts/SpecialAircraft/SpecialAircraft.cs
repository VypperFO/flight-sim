using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02.model.Aircrafts;

namespace Tp_02.model.Aircrafts.SpecialAircraft
{
    [Serializable]
    public class SpecialAircraft : Aircraft
    {
        protected Airport DepartureAirport;

        public SpecialAircraft(Airport departureAirport)
        {
            DepartureAirport = departureAirport;
        }
    }
}
