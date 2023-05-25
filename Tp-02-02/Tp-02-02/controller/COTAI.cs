using System.Xml.Serialization;
using Tp_02_02.model;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Clients;
using Tp_02_02.model.Clients.SpecialClients;
using Tp_02_02.model.States;

namespace Tp_02_02.controller
{
    public class COTAI
    {
        public FormSimulator simulatorForm;
        public Scenario scenario;
        public bool isSet;

        [STAThread]
        static void Main()
        {
            COTAI cotai = new COTAI();
        }

        private void init()
        {
            Thread.Sleep(scenario.speed);
            State currentState = scenario.GetState();

            if (currentState is PlayingState)
            {
                simulatorForm.clearAll();
                Console.WriteLine(scenario.time);
                List<Airport> airports = scenario.AirportList;
                scenario.time = scenario.time + 60;
                scenario = scenario.PerformOperations();
                simulatorForm.setClients(simulatorForm.getListbox1Selected());
                simulatorForm.setTime(scenario.giveMeTheTime());

                foreach (Airport airport in airports)
                {
                    simulatorForm.PlaceAirportsOnMap(airport.Coords, airport.Name);

                    // Fonction pour afficher les feux et les secours
                    // (!!! ATTENTION, TRES MAL OPTIMIZER CAR CREATION DE NOMBREUX OBJETS PictureBox ET Image !!!)
                    //placeFireAndRescueOnMap(airport);

                    foreach (Aircraft aircraft in airport.AircraftList)
                    {
                        if (aircraft.GetState().GetType().Name.Equals("FlyingState"))
                        {
                            if (!isSet)
                            {
                                simulatorForm.MovePlane(aircraft.StartingPosition);
                                isSet = true;
                            }
                            else
                            {
                                simulatorForm.MovePlane(aircraft.CurrentPosition);
                                Point currentPosition = new((int)aircraft.StartingPosition.X, (int)aircraft.StartingPosition.Y);
                                Point destinationPoint = new((int)aircraft.Destination.X, (int)aircraft.Destination.Y);
                                simulatorForm.DrawLine(currentPosition, destinationPoint, GetAircraftLineColor(aircraft));
                            }
                        }
                    }
                }

            }
            init();
        }

        public COTAI()
        {
            ApplicationConfiguration.Initialize();
            scenario = new Scenario();
            simulatorForm = new FormSimulator(this);
            Thread newThread = new(init);
            newThread.Start();
            Application.Run(simulatorForm);
        }

        public void Load(string filePath)
        {
            simulatorForm.clearListboxes();
            XmlSerializer xs = new(typeof(Scenario));
            using (StreamReader rd = new(filePath))
            {
                scenario = xs.Deserialize(rd) as Scenario;
                List<Airport> airports = scenario.AirportList;
                foreach (Airport airport in airports)
                {
                    foreach (var aircraft in airport.AircraftList)
                    {
                        aircraft.StartingPosition = airport.ConvertFromGPSToCoords(airport.Coords);
                        aircraft.CurrentPosition = airport.ConvertFromGPSToCoords(airport.Coords);
                    }
                    simulatorForm.PlaceAirportsOnMap(airport.Coords, airport.Name);
                    airport.InjectClients(airports);

                }
                simulatorForm.setAirportsName();
            }

            simulatorForm.SetPlayBtnEnable(true);
            scenario.changeState(new ReadyState(scenario));
        }

        public void Play()
        {
            scenario.Play();
        }

        public void Forward()
        {
            scenario.Forward();
        }

        public string[] AirportsToStrings()
        {
            List<Airport> airports = scenario.AirportList;
            String[] airportstring = new string[airports.Count];
            for (int i = 0; i < airports.Count; i++)
            {
                airportstring[i] = (airports[i].ToString());
            }
            return airportstring;
        }


        public void placeFireAndRescueOnMap(Airport airport)
        {
            foreach (Client client in airport.ClientList)
            {
                if (client.GetType().Name.Equals("FireClient"))
                {
                    SpecialClient specialClient = (SpecialClient)client;
                    float GPSx = specialClient.Position.X;
                    float GPSy = specialClient.Position.Y;
                    simulatorForm.PlaceFire(GPSx, GPSy);
                }
                if (client.GetType().Name.Equals("RescueClient"))
                {
                    Console.WriteLine("salut");
                    SpecialClient specialClient = (SpecialClient)client;
                    float GPSx = specialClient.Position.X;
                    float GPSy = specialClient.Position.Y;
                    simulatorForm.PlaceRescue(GPSx, GPSy);
                }
            }

        }

        public void IncreaseSpeed()
        {
            scenario.speed = (scenario.speed / 2);
        }

        public void DecreaseSpeed()
        {
            scenario.speed = (scenario.speed * 2);
        }

        private Color GetAircraftLineColor(Aircraft aircraft)
        {
            string aircraftType = aircraft.GetType().Name;
            switch (aircraftType)
            {
                case "TankAircraft":
                    return Color.Yellow;
                case "HelicopterAircraft":
                    return Color.Red;
                case "ObserverAircraft":
                    return Color.Gray;
                case "PassengerAircraft":
                    return Color.Green;
                case "CargoAircraft":
                    return Color.Blue;
                default:
                    return Color.Black;
            }
        }

    }
}