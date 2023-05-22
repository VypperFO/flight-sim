using System.Xml.Serialization;
using Tp_02_02.model;
using Tp_02_02.model.States;

namespace Tp_02_02.controller
{
    public class COTAI
    {
        public FormSimulator simulatorForm;
        public Scenario scenario;

        [STAThread]
        static void Main()
        {
            COTAI cotai = new COTAI();
        }

        private void init()
        {
            scenario.speed = 1000;

            while (true)
            {
                if (scenario.GetState() is PlayingState)
                {
                    Console.WriteLine("cum");

                    // toute la logique ce fait dans cette fonction lah
                    scenario = scenario.playing();

                    // TODO fonction update avion
                } else
                {
                    Console.WriteLine("not cum");
                }

                Thread.Sleep(scenario.speed);
            }
        }

        public COTAI()
        {
            scenario= new Scenario();
            ApplicationConfiguration.Initialize();
            simulatorForm = new FormSimulator(this);
            Thread newThread = new(init);
            newThread.Start();
            Application.Run(simulatorForm);
        }

        public void Load(string filePath)
        {
            XmlSerializer xs = new(typeof(Scenario));
            using (StreamReader rd = new(filePath))
            {
                scenario = xs.Deserialize(rd) as Scenario;
                List<Airport> airports = scenario.AirportList;

                for (int i = 0; i < airports.Count; i++)
                {
                    Console.WriteLine(airports[i].Name + ", ");
                    Console.WriteLine(simulatorForm.ConvertFromGPSToCoords(airports[i].Coords + ", "));
                    simulatorForm.PlaceOnMap(airports[i].Coords, airports[i].Name);

                    for (int j = 0; j < scenario.AirportList[i].AircraftList.Count; j++)
                    {
                        Console.WriteLine("\t- " + scenario.AirportList[i].AircraftList[j].Name);
                    }
                }
            }

            simulatorForm.SetPlayBtnEnable(true);

            // Change state MUY IMPORTANTO
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
    }
}