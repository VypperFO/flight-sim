using System.Xml.Serialization;
using System;
using Tp_02_02.model;

namespace Tp_02_02.controller
{
    public class SimulatorController
    {

        [STAThread]
        static void Main()
        {
            SimulatorController controller = new SimulatorController();
            controller.load("something");
        }

        public SimulatorController()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormSimulator());
        }

        public void load(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Scenario));
            using (StreamReader rd = new StreamReader("scenario.xml"))
            {
                Scenario scenario = xs.Deserialize(rd) as Scenario;
                for (int i = 0; i < scenario.AirportList.Count; i++)
                {
                    Console.WriteLine(scenario.AirportList[i].Name + ":");
                    for (int j = 0; j < scenario.AirportList[i].AircraftList.Count; j++)
                    {
                        Console.WriteLine("\t- " + scenario.AirportList[i].AircraftList[j].Name);
                    }
                }
            }
        }
    }
}