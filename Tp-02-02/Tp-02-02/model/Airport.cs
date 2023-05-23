using System.Xml.Serialization;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.TransportAircrafts;
using Tp_02_02.model.Clients;

namespace Tp_02_02.model
{
    public class Airport
    {
        public string Name { get; set; }
        public string Coords { get; set; }
        public int MinPassenger { get; set; }
        public int MaxPassenger { get; set; }
        public int MinMerchandise { get; set; }
        public int MaxMerchandise { get; set; }
        [XmlArray("AircraftList")]
        [XmlArrayItem("HelicopterAircraft", typeof(HelicopterAircraft))]
        [XmlArrayItem("ObserverAircraft", typeof(ObserverAircraft))]
        [XmlArrayItem("TankAircraft", typeof(TankAircraft))]
        [XmlArrayItem("CargoAircraft", typeof(CargoAircraft))]
        [XmlArrayItem("PassengerAircraft", typeof(PassengerAircraft))]
        [XmlArrayItem("Aircraft", typeof(Aircraft))]
        public List<Aircraft> AircraftList { get; set; }
        public List<Client> ClientDictionary { get; set; }

        public Airport()
        {
            AircraftList = new List<Aircraft>();
        }

        public Airport(string name, string coords, int minPassenger, int maxPassenger, int minMerchandise, int maxMerchandise)
        {
            Name = name;
            Coords = coords;
            MinPassenger = minPassenger;
            MaxPassenger = maxPassenger;
            MinMerchandise = minMerchandise;
            MaxMerchandise = maxMerchandise;
            AircraftList = new List<Aircraft>();
        }

        public override string ToString()
        {
            string temp = Name + "," + Coords.ToString() + "," + MinPassenger.ToString() + "," + MaxPassenger.ToString() + "," + MinMerchandise.ToString() + "," + MaxMerchandise.ToString() + "," + MinPassenger.ToString() + ".";
            foreach (var item in AircraftList)
            {
                temp += item.ToString() + ",";
            }
            return temp;
        }

        public void InjectClients(List<Airport> airports)
        {
            Random rand = new Random();
            int randAirportIndex = rand.Next(airports.Count);
            int randRangePassenger = rand.Next(MinPassenger, MaxPassenger);
            int randRangeMerchandise = rand.Next(MinMerchandise, MaxMerchandise);
            int randNumberIterations = rand.Next(1, 3);

            ClientFactory clientFactory = ClientFactory.Instance;

            for (int i = 0; i < randNumberIterations; i++)
            {
                Client passengerClient = clientFactory.CreateClient("Passenger", airports[randAirportIndex]);
                Client merchandiseClient = clientFactory.CreateClient("Cargo", airports[randAirportIndex]);

                ClientDictionary.Add(passengerClient);
                ClientDictionary.Add(merchandiseClient);
            }

            for (int i = 0; i < airports.Count; i++)
            {
                Console.WriteLine($"Clients: {ClientDictionary[i]}");
            }
            // TODO add clients de facon aleatoire
            // Type de clients a injecter: cargo et passager avec destination aleatoire
        }
    }
}
