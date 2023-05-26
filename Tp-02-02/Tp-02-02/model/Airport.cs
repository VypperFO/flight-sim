using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.States;
using Tp_02_02.model.Aircrafts.TransportAircrafts;
using Tp_02_02.model.Clients;
using Tp_02_02.model.Clients.SpecialClients;
using Tp_02_02.model.Clients.TransportClients;

namespace Tp_02_02.model
{
    /// <summary>
    /// Classe aeroport contenant plusieurs type d'avion et client
    /// </summary>
    public class Airport
    {
        public string Name { get; set; } // nom de l'aeroport
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
            ClientList = new List<Client>();
        }

        public List<Airport> RunAirport(List<Airport> airports)
        {
            //double threshold = 0.8; // when at 80% of capacity
            for (int i = 0; i < ClientList.Count; i++)
            {
                if (ClientList[i] is CargoClient)
                {
                    int cargoIndex = GetAircraftIndex("cargo");
                    TransportDeparture(airports, cargoIndex, i);
                }
                else if (ClientList[i] is PassengerClient)
                {
                    int passIndex = GetAircraftIndex("passenger");
                    TransportDeparture(airports, passIndex, i);
                }
                else if (ClientList[i] is RescueClient)
                {
                    int rescueIndex = GetAircraftIndex("helicopter");
                    SpecialDeparture(airports, rescueIndex, i);
                }
                else if (ClientList[i] is ObserverClient)
                {
                    int observerIndex = GetAircraftIndex("observer");
                    SpecialDeparture(airports, observerIndex, i);
                }
                else if (ClientList[i] is FireClient)
                {
                    int fireIndex = GetAircraftIndex("fire");
                    SpecialDeparture(airports, fireIndex, i, true);
                }
            }

            for (int i = 0; i < AircraftList.Count; i++)
            {
                if (AircraftList[i].GetState().GetType().Name.Equals("FlyingState"))
                {
                    AircraftList[i].GoTo(AircraftList[i].Destination);
                }
            }

            return airports;
        }

        private int GetAircraftIndex(string aircraft)
        {
            switch (aircraft.ToLower())
            {
                case "helicopter":
                    return AircraftList.FindIndex(a => a is HelicopterAircraft && ((HelicopterAircraft)a).GetState() is not FlyingState);
                case "cargo":
                    return AircraftList.FindIndex(a => a is CargoAircraft && ((CargoAircraft)a).GetState() is not FlyingState);
                case "passenger":
                    return AircraftList.FindIndex(a => a is PassengerAircraft && ((PassengerAircraft)a).GetState() is not FlyingState);
                case "fire":
                    return AircraftList.FindIndex(a => a is TankAircraft && ((TankAircraft)a).GetState() is not FlyingState);
                case "observer":
                    return AircraftList.FindIndex(a => a is ObserverAircraft && ((ObserverAircraft)a).GetState() is not FlyingState);
                default:
                    return -1;
            }
        }

        private void TransportDeparture(List<Airport> airports, int aircraftIndex, int clientIndex)
        {
            if (aircraftIndex != -1)
            {
                Aircraft aircraft = AircraftList[aircraftIndex];
                TransportClient transportClient = (TransportClient)ClientList[clientIndex];


                // Change state
                aircraft.changeState(new FlyingState(aircraft));
                //Console.WriteLine($"State changed: {aircraft.GetState}");

                // Change destination
                aircraft.Destination = ConvertFromGPSToCoords(transportClient.Destination.Coords);
                // Console.WriteLine($"The plane {aircraft.Name} from {aircraft.StartingPosition} is now flying towards {aircraft.Destination}");

                // Add plane to other airport
                int airportDestinationIndex = airports.FindIndex(a => a.Name.Equals(transportClient.Destination.Name, StringComparison.OrdinalIgnoreCase));
                airports[airportDestinationIndex].AircraftList.Add(aircraft);

                // Remove client and plane
                transportClient.NumberOfClients -= aircraft.Capacity;

                if (transportClient.NumberOfClients <= 0)
                {
                    ClientList.RemoveAt(clientIndex);
                }

                AircraftList.RemoveAt(aircraftIndex);
            }
        }

        private void SpecialDeparture(List<Airport> airports, int aircraftIndex, int clientIndex, bool IsFire = false)
        {
            if (aircraftIndex != -1)
            {
                SpecialAircraft aircraft = (SpecialAircraft)AircraftList[aircraftIndex];

                if (!IsFire)
                {
                    SpecialClient specialClient = (SpecialClient)ClientList[clientIndex];

                    // Change state
                    aircraft.changeState(new FlyingState(aircraft));
                    Console.WriteLine($"State of {aircraft.Name} changed: {aircraft.GetState().GetType().Name}");

                    // Change destination
                    aircraft.Destination = specialClient.Position;

                    ClientList.RemoveAt(clientIndex);
                }
                else
                {
                    FireClient fireClient = (FireClient)ClientList[clientIndex];

                    // Change state
                    aircraft.changeState(new FlyingState(aircraft));

                    // Change destination
                    aircraft.Destination = fireClient.Position;

                    fireClient.Intensity--;
                    if (fireClient.Intensity == 0)
                    {
                        ClientList.RemoveAt(clientIndex);
                    }
                }
            }
        }

        public void InjectClients(List<Airport> airports)
        {
            ClientFactory clientFactory = new();
            Random rand = new Random();
            int randNumberIterations = rand.Next(1, 6);

            for (int i = 0; i < randNumberIterations; i++)
            {
                int randRangePassenger = rand.Next(MinPassenger, MaxPassenger);
                int randRangeMerchandise = rand.Next(MinMerchandise, MaxMerchandise);

                Airport randAirport = airports[rand.Next(airports.Count)];

                // Removes itself from possible airport destination
                while (randAirport == this)
                {
                    randAirport = airports[rand.Next(airports.Count)];
                }

                TransportClient passengerClient = clientFactory.CreateTransportClient("Passenger");
                passengerClient.Destination = randAirport;
                passengerClient.NumberOfClients = randRangePassenger;

                TransportClient merchandiseClient = clientFactory.CreateTransportClient("Cargo");
                merchandiseClient.Destination = randAirport;
                merchandiseClient.NumberOfClients = randRangeMerchandise;

                Client existingPassengerClient = ClientList.Find(client => client is TransportClient transportClient && transportClient.Destination == passengerClient.Destination);
                Client existingMerchandiseClient = ClientList.Find(client => client is TransportClient transportClient && transportClient.Destination == merchandiseClient.Destination);

                if (existingPassengerClient != null)
                {
                    if (existingPassengerClient is TransportClient transportExistingClient)
                    {
                        transportExistingClient.NumberOfClients += passengerClient.NumberOfClients;
                    }
                }
                else
                {
                    ClientList.Add(passengerClient);
                }

                if (existingMerchandiseClient != null)
                {
                    if (existingPassengerClient is TransportClient transportExistingClient)
                    {
                        transportExistingClient.NumberOfClients += merchandiseClient.NumberOfClients;
                    }
                }
                else
                {
                    ClientList.Add(merchandiseClient);
                }
            }
        }

        public bool contains(string aircraftType)
        {
            int index = GetAircraftIndex(aircraftType);

            if (index != -1)
            {
                return true;
            }

            return false;
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
