using System.Xml.Serialization;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.TransportAircrafts;
using Tp_02_02.model.Clients;
using Tp_02_02.model.Clients.TransportClients;

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
        public List<Client> ClientList { get; set; }

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

        public void InjectClients(List<Airport> airports)
        {
            Random rand = new Random();
            int randNumberIterations = rand.Next(1, 6);

            ClientFactory clientFactory = new();

            for (int i = 0; i < randNumberIterations; i++)
            {
                int randRangePassenger = rand.Next(MinPassenger, MaxPassenger);
                int randRangeMerchandise = rand.Next(MinMerchandise, MaxMerchandise);

                TransportClient passengerClient = clientFactory.CreateTransportClient("Passenger");
                /// ENLEVER QUE LA DESTINATION PEU ETRE SOIS MEME
                passengerClient.Destination = airports[rand.Next(airports.Count)];
                passengerClient.NumberOfClients = randRangePassenger;

                TransportClient merchandiseClient = clientFactory.CreateTransportClient("Cargo");
                merchandiseClient.Destination = airports[rand.Next(airports.Count)];
                merchandiseClient.NumberOfClients = randRangeMerchandise;

                ClientList.Add(passengerClient);
                ClientList.Add(merchandiseClient);
            }
        }

        public override string ToString()
        {
            string temp = Name + "," + Coords.ToString() + "," + MinPassenger.ToString() + "," + MaxPassenger.ToString() + "," + MinMerchandise.ToString() + "," + MaxMerchandise.ToString() + "," + MinPassenger.ToString() + ".";
            foreach (var item in AircraftList)
            {
                temp += item.ToString() + ",";
            }
            temp += ";";
            foreach (var item in ClientList)
            {
                temp += item.ToString();
            }
            return temp;
        }
    }
}
