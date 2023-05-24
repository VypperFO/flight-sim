namespace Tp_02_02.model.Clients.TransportClients
{
    public class TransportClient : Client
    {
        public Airport Destination { get; set; }
        public int NumberOfClients { get; set; }

        public TransportClient() { }

        public override string ToString()
        {
            return Destination.Name + "," + NumberOfClients.ToString() + ".";
        }
    }
}
