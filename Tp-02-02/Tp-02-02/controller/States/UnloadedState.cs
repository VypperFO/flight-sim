using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tp_02_02.model;

namespace Tp_02_02.controller.States
{
    public class UnloadedState : State
    {
        public UnloadedState(COTAI sim)
        {
            this.sim = sim;
        }

        public override void Load(string filePath)
        {
            Scenario scenario = new();
            XmlSerializer xs = new(typeof(Scenario));
            using (StreamReader rd = new(filePath))
            {
                scenario = xs.Deserialize(rd) as Scenario;
                List<Airport> airports = scenario.AirportList;

                for (int i = 0; i < airports.Count; i++)
                {
                    Console.WriteLine(airports[i].Name + ", ");
                    Console.WriteLine(sim.simulatorForm.ConvertFromGPSToCoords(airports[i].Coords + ", "));
                    sim.simulatorForm.PlaceOnMap(airports[i].Coords, airports[i].Name);

                    for (int j = 0; j < scenario.AirportList[i].AircraftList.Count; j++)
                    {
                        Console.WriteLine("\t- " + scenario.AirportList[i].AircraftList[j].Name);
                    }
                }
            }

            sim.scenario = scenario;
            sim.simulatorForm.SetPlayBtnEnable(true);

            // Change state MUY IMPORTANTO
            sim.changeState(new ReadyState(sim));
        }

        // TODO tell return error -> you have to load
        public override void Play() { }

        // TODO tell return error -> you have to load
        public override void Stop() { }

        // TODO tell return error -> you have to load
        public override void Forward() { }
    }
}
