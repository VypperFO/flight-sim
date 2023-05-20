using System.Xml.Serialization;
using Tp_02.model;
using Tp_02.model.Aircrafts;

namespace Tp_02.controller
{
    public class GeneratorController
    {

        FormGenerator FormGen;
        Scenario scenario = new();
        [STAThread]
        static void Main()
        {
            GeneratorController controller = new GeneratorController();
        }

        public GeneratorController()
        {
            ApplicationConfiguration.Initialize();
            FormGen = new FormGenerator();
            FormGen.GenController = this;
            Application.Run(FormGen);
        }

        public void AddAirport(string[] airport)
        {
            Airport newAirport = new Airport();
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

            Airport currentAirport = scenario.AirportList.FirstOrDefault(airport => airport.Name == airportName);

            if (currentAirport != null)
            {
                AircraftFactory aircraftFactory = AircraftFactory.GetAircraftFactory;
                Aircraft newAircraft = aircraftFactory.CreateAircraft(aircraft[1]);
                newAircraft.Name = aircraft[0];
                newAircraft.Capacity = Int32.Parse(aircraft[2]);
                currentAirport.AircraftList.Add(newAircraft);

                for (int i = 0; i < scenario.AirportList.Count; i++)
                {
                    Console.WriteLine(scenario.AirportList[i].Name + ":");
                    for (int j = 0; j < scenario.AirportList[i].AircraftList.Count; j++)
                    {
                        Console.WriteLine("\t- " + scenario.AirportList[i].AircraftList[j].Name);
                    }
                }
                Console.WriteLine("------------------------------------------------------------------");
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
    }
}