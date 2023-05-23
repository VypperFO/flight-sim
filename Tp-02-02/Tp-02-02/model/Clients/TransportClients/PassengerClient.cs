using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_02_02.model.Clients.TransportClients
{
    public class PassengerClient: TransportClient
    {
        public PassengerClient(Airport airportDestination)
        {
            this.Destination = airportDestination;
        }
    }
}
