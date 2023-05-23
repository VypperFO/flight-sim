using System.Xml.Serialization;
using Tp_02.model;
using Tp_02.model.Aircrafts;

namespace Tp_02.controller
{
    /// <summary>
    ///  Genere, initialise et serialise tout ce qui va des le scenario.
    /// </summary>
    public class GeneratorController
    {

        FormGenerator FormGen; // Formulaire du generateur
        Scenario scenario = new(); // scenario qui sera serialiser
        List<string> aircraftsNames; // liste de nom d'aeroport deja utiliser
        List<string> airportsNames; // liste de nom d'avion deja utiliser

        [STAThread]
        static void Main()
        {
            GeneratorController controller = new GeneratorController();
        }

        /// <summary>
        /// 
        /// </summary>
        public GeneratorController()
        {
            ApplicationConfiguration.Initialize();
            FormGen = new FormGenerator();
            FormGen.GenController = this;
            aircraftsNames = new List<string>();
            airportsNames = new List<string>();
            Application.Run(FormGen);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="airport"></param>
        public void AddAirport(string[] airport)
        {
            Airport newAirport = new Airport();
            airportsNames.Add(airport[0]);
            newAirport.Name = airport[0];
            newAirport.Coords = airport[1];
            newAirport.MinPassenger = Int32.Parse(airport[2]);
            newAirport.MaxPassenger = Int32.Parse(airport[3]);
            newAirport.MinMerchandise = Int32.Parse(airport[4]);
            newAirport.MaxMerchandise = Int32.Parse(airport[5]);

            scenario.AirportList.Add(newAirport);

        }

        public void AddAirplane(string airportName, string[] aircraft)
        {
            Airport ?currentAirport = scenario.AirportList.FirstOrDefault(airport => airport.Name == airportName);

            if (currentAirport != null)
            {
                aircraftsNames.Add(aircraft[0]);

                AircraftFactory aircraftFactory = AircraftFactory.GetAircraftFactory;
                Aircraft newAircraft = aircraftFactory.CreateAircraft(aircraft[1], currentAirport.Coords);

                newAircraft.Name = aircraft[0];
                newAircraft.Capacity = int.Parse(aircraft[2]);
                currentAirport.AircraftList.Add(newAircraft);
            }
            else
            {
                throw new NullReferenceException($"No airport with the name {airportName} found");
            }
        }

        public void GenerateScenario()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Scenario));
            using (StreamWriter wr = new StreamWriter("scenario.xml"))
            {
                xs.Serialize(wr, scenario);
            }
        }

        public List<string[]> GetAirplanList(string airportName)
        {
            List<string[]> aircraftList = new List<string[]>();
            List<Aircraft> tempAircraftList = new List<Aircraft>();

            for (int i = 0; i < scenario.AirportList.Count; i++)
            {
                if (scenario.AirportList[i].Name == airportName)
                {
                    tempAircraftList = scenario.AirportList[i].AircraftList;
                    foreach (Aircraft a in tempAircraftList)
                    {
                        string type = "";
                        switch (a.GetType().ToString())
                        {
                            case "Tp_02.model.Aircrafts.TransportAircraft.CargoAircraft":
                                type = "Cargo";
                                break;
                            case "Tp_02.model.Aircrafts.TransportAircraft.PassengerAircraft":
                                type = "Passager";
                                break;
                            case "Tp_02.model.Aircrafts.SpecialAircraft.HelicopterAircraft":
                                type = "Helicopter";
                                break;
                            case "Tp_02.model.Aircrafts.SpecialAircraft.ObserverAircraft":
                                type = "Observeur";
                                break;
                            case "Tp_02.model.Aircrafts.SpecialAircraft.TankAircraft":
                                type = "Citerne";
                                break;
                        }
                        string[] aircraft = { a.Name, type, a.Capacity.ToString() };
                        aircraftList.Add(aircraft);
                    }
                }

            }
            return aircraftList;
        }

        public bool IsAircraftNameExistent(string aircraftName)
        {

            if (aircraftsNames.Contains(aircraftName))
            {
                return true;
            }
            return false;
        }

        public bool IsAirportNameExistent(string airportName)
        {

            if (airportsNames.Contains(airportName))
            {
                return true;
            }
            return false;
        }
    }
}