using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_02_02.model.Clients.TransportClients
{
    public class TransportClient: Client
    {
        public Airport Destination { get; set; }
        public int NumberOfClients { get; set; }

        public TransportClient() { }
    }
}
