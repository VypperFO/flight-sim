using System.Numerics;
using Tp_02_02.model.Clients.SpecialClients;
using Tp_02_02.model.Clients.TransportClients;

namespace Tp_02_02.model.Clients
{
    public class ClientFactory
    {
        public ClientFactory()
        {
        }

        public Client CreateClient(string clientType)
        {
            switch (clientType)
            {
                case "Rescue":
                    return new RescueClient();
                case "Fire":
                    return new FireClient();
                case "Observer":
                    return new ObserverClient();
                case "Cargo":
                    return new CargoClient();
                case "Passenger":
                    return new PassengerClient();
                default:
                    throw new ArgumentException($"Invalid client type: {clientType}");
            }
        }

        public TransportClient CreateTransportClient(string clientType)
        {
            switch (clientType)
            {
                case "Cargo":
                    return new CargoClient();
                case "Passenger":
                    return new PassengerClient();
                default:
                    throw new ArgumentException($"Invalid client type: {clientType}");
            }
        }

        public SpecialClient CreateSpecialClientWithRandomPos(string clientType)
        {
            SpecialClient specialClient;

            switch (clientType)
            {
                case "Rescue":
                    specialClient = new RescueClient();
                    break;
                case "Fire":
                    specialClient = new FireClient();
                    break;
                case "Observer":
                    specialClient = new ObserverClient();
                    break;
                default:
                    throw new ArgumentException($"Invalid client type: {clientType}");
            }

            Random rand = new();

            // 600 because of map width
            int randX = rand.Next(1, 600);

            // 600 because of map width
            int randY = rand.Next(1, 600);

            Vector2 v;
            v.X = randX;
            v.Y = randY;
            specialClient.Position = v;

            return specialClient;
        }
    }
}
