using System.Xml.Serialization;
using Tp_02_02.model;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Clients;
using Tp_02_02.model.Clients.SpecialClients;
using Tp_02_02.model.States;

namespace Tp_02_02.controller
{
    /// <summary>
    /// Controlleur du programme
    /// </summary>
    public class COTAI
    {
        private FormSimulator simulatorForm; // form simulation du programme
        private Scenario scenario; // scenario actuel du programme
        private bool isSet; // si les avions on ete placer sur la map

        [STAThread]
        static void Main()
        {
            COTAI cotai = new COTAI();
        }

        /// <summary>
        /// intialise le controlleur et le scenario
        /// </summary>
        public COTAI()
        {
            ApplicationConfiguration.Initialize();
            scenario = new Scenario();
            simulatorForm = new FormSimulator(this);
            Thread newThread = new(init);
            newThread.Start();
            Application.Run(simulatorForm);
        }

        #region Main operations

        /// <summary>
        /// roule 1 tick de tout le programme a l'infinie
        /// </summary>
        private void init()
        {
            Thread.Sleep(scenario.speed);
            State currentState = scenario.GetState();

            if (currentState is PlayingState)
            {
                simulatorForm.clearAll();
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
                    // placeFireAndRescueOnMap(airport);

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

        /// <summary>
        /// initialise les aerport avion et clients au load du programme
        /// </summary>
        /// <param name="filePath">path du fichier choissis</param>
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

        #endregion

        #region Utils

        /// <summary>
        /// dit au scenario de jouer
        /// </summary>
        public void Play()
        {
            scenario.Play();
        }
        
        /// <summary>
        /// dit au scenario davancer plus vite
        /// </summary>
        public void Forward()
        {
            scenario.Forward();
        }

        /// <summary>
        /// transforme tout les string aeroport en tableau de string
        /// </summary>
        /// <returns>tout les aeroport sous forme de string dans un tableau</returns>
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

        /// <summary>
        /// place le rescue et les feux sur la carte
        /// </summary>
        /// <param name="airport">l'aeroport assigner a ces dernier</param>
        private void placeFireAndRescueOnMap(Airport airport)
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

        /// <summary>
        /// augmente la vitesse du programme
        /// </summary>
        public void IncreaseSpeed()
        {
            scenario.speed = (scenario.speed / 2);
        }

        /// <summary>
        /// ralentie la vitesse du programme
        /// </summary>
        public void DecreaseSpeed()
        {
            scenario.speed = (scenario.speed * 2);
        }

        /// <summary>
        /// donne la couleur que la ligne de vol doit etre en dependant du type d'avion
        /// </summary>
        /// <param name="aircraft">type d'avion</param>
        /// <returns>la couleur de la ligne</returns>
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

        #endregion 
    }
}