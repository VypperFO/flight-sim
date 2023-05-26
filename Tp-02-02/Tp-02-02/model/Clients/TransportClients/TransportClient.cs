namespace Tp_02_02.model.Clients.TransportClients
{
    /// <summary>
    /// Classe des clients transportable sois passager et cargo
    /// </summary>
    public class TransportClient : Client
    {
        public Airport Destination { get; set; } // destination du client
        public int NumberOfClients { get; set; } // nombre de client;

        public TransportClient() { }

        /// <summary>
        /// Envoie les donnes membre du client sous forme de string
        /// </summary>
        /// <returns>les donnes membre du client sous forme de string</returns>
        public override string ToString()
        {
            if (this is PassengerClient)
            {
                return Destination.Name + "," + NumberOfClients.ToString() + "," + "passager" + ".";
            }
            return Destination.Name + "," + NumberOfClients.ToString() + "," + "cargo" + ".";
        }
    }
}
