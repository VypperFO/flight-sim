using System.Xml.Serialization;
using Tp_02_02.model;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Aircrafts.States;
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
                scenario.time = scenario.time + 60;
                List<Airport> airports = scenario.AirportList;
                for (int i = 0; i < airports.Count; i++)
                {
                    simulatorForm.PlaceOnMap(airports[i].Coords, airports[i].Name);
                }
                foreach (Airport airport in airports)
                {
                    foreach (var aircraft in airport.AircraftList)
                    {
    
                        if (aircraft.GetState() is FlyingState)
                        {
                            if (isSet == false)
                            {
                                simulatorForm.MovePlane(aircraft.StartingPosition);
                                isSet = true;
                            }
                            else
                            {
                                simulatorForm.MovePlane(aircraft.CurrentPosition);
                            }
                        }
                    }
                }
                scenario = scenario.PerformOperations();
                simulatorForm.setClients(simulatorForm.getListbox1Selected());
                placeFireOnMap();
                placeRescueOnMap();
                simulatorForm.setTime(scenario.giveMeTheTime());
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

                }
                simulatorForm.setAirportsName();
                for (int i = 0; i < airports.Count; i++)
                {
                    airports[i].InjectClients(airports);
                    simulatorForm.PlaceOnMap(airports[i].Coords, airports[i].Name);
                }
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


        public void placeFireOnMap()
        {
            List<SpecialClient> SpecialClientList = scenario.SpecialClientList;
            foreach (SpecialClient client in SpecialClientList)
            {
                if (client.GetType().ToString() == "Tp_02_02.model.Clients.SpecialClients.FireClient")
                {
                    float GPSx = client.Position.X;
                    float GPSy = client.Position.Y;
                    simulatorForm.PlaceFire(GPSx, GPSy);
                }
            }

        }

        public void placeRescueOnMap()
        {
            List<SpecialClient> SpecialClientList = scenario.SpecialClientList;
            foreach (SpecialClient client in SpecialClientList)
            {
                if (client.GetType().ToString() == "Tp_02_02.model.Clients.SpecialClients.RescueClient")
                {
                    float GPSx = client.Position.X;
                    float GPSy = client.Position.Y;
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

    }
}