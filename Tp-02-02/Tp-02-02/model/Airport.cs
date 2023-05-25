using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.States;
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


        public List<Airport> RunAirport(List<Airport> airports)
        {
            //double threshold = 0.8; // when at 80% of capacity
            for (int i = 0; i < ClientList.Count; i++)
            {
                if (ClientList[i] is CargoClient)
                {
                    // find first cargo aircraft
                    int cargoIndex = AircraftList.FindIndex(a => a is CargoAircraft && ((CargoAircraft)a).GetState() is not FlyingState);

                    TransportDeparture(airports, cargoIndex, i);
                }

                else if (ClientList[i] is PassengerClient)
                {
                    // find first cargo aircraft
                    int passIndex = AircraftList.FindIndex(a => a is PassengerAircraft && ((PassengerAircraft)a).GetState() is not FlyingState);

                    TransportDeparture(airports, passIndex, i);

                }

                /*if (ClientList[i] is FireClient)
                {
                    // get the first tank plane, call togo() method, remove from current airport, add special client as destination
                }*/


            }

            for (int i = 0; i < AircraftList.Count; i++)
            {
                //if (AircraftList[i].GetState() is FlyingState)
                //{
                AircraftList[i].GoTo(AircraftList[i].Destination);
                //}
            }

            return airports;
        }

        private void TransportDeparture(List<Airport> airports, int aircraftIndex, int clientIndex)
        {
            if (aircraftIndex != -1)
            {
                Aircraft aircraft = AircraftList[aircraftIndex];
                TransportClient cargoClient = (TransportClient)ClientList[clientIndex];

                // Change state
                aircraft.changeState(new FlyingState(aircraft));
                Console.WriteLine($"State changed: {aircraft.GetState}");

                // Change destination
                aircraft.Destination = ConvertFromGPSToCoords(cargoClient.Destination.Coords);
                Console.WriteLine($"The plane {aircraft.Name} from {aircraft.StartingPosition} is now flying towards {aircraft.Destination}");

                // Add plane to other airport
                int airportDestinationIndex = airports.FindIndex(a => a.Name.Equals(cargoClient.Destination.Name, StringComparison.OrdinalIgnoreCase));
                airports[airportDestinationIndex].AircraftList.Add(aircraft);

                // Remove client and plane
                ClientList.RemoveAt(clientIndex);
                AircraftList.RemoveAt(aircraftIndex);
            }
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
                Airport randAirport = airports[rand.Next(airports.Count)];

                // Removes itself from possible airport destination
                while (randAirport == this)
                {
                    randAirport = airports[rand.Next(airports.Count)];
                }

                passengerClient.Destination = randAirport;
                passengerClient.NumberOfClients = randRangePassenger;

                TransportClient merchandiseClient = clientFactory.CreateTransportClient("Cargo");
                merchandiseClient.Destination = randAirport;
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
                if (item is TransportClient || item is CargoClient)
                {
                    temp += item.ToString();
                }
            }
            return temp;
        }

        public Vector2 ConvertFromGPSToCoords(string gpsCoords)
        {
            string pattern = @"-?\d+";

            MatchCollection matches = Regex.Matches(gpsCoords, pattern);

            int[] numbers = new int[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                numbers[i] = int.Parse(matches[i].Value);
            }


            float longitudeDegrees = numbers[0];
            float longitudeMinutes = numbers[1];
            float longitudeSeconds = numbers[2];

            float latitudeDegrees = numbers[3];
            float latitudeMinutes = numbers[4];
            float latitudeSeconds = numbers[5];

            float mapWidth = 600;
            float mapHeight = 600;

            float latitude = ConvertToDecimalDegrees(latitudeDegrees, latitudeMinutes, latitudeSeconds);
            float longitude = ConvertToDecimalDegrees(longitudeDegrees, longitudeMinutes, longitudeSeconds);

            Vector2 vector = new();
            vector.X = ConvertToX(longitude, mapWidth);
            vector.Y = ConvertToY(latitude, mapHeight);


            return vector;
        }

        private float ConvertToDecimalDegrees(float degrees, float minutes, float seconds)
        {
            float decimalDegrees = degrees + (minutes / 60) + (seconds / 3600);
            return decimalDegrees;
        }

        private float ConvertToX(float longitude, float mapWidth)
        {
            float x = (longitude + 180) * (mapWidth / 360);
            return x;
        }

        private float ConvertToY(float latitude, float mapHeight)
        {
            float y = (90 - latitude) * (mapHeight / 180);
            return y;
        }
    }
}
