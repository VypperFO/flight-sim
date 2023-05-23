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
            while (true)
            {
                scenario.PerformOperations();
            }
        }

        public COTAI()
        {
            ApplicationConfiguration.Initialize();
            scenario= new Scenario();
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
                simulatorForm.setAirportsName();

                for (int i = 0; i < airports.Count; i++)
                {
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

        public string[] airportsToStrings()
        {
            List<Airport> airports = scenario.AirportList;
            String[] airportstring = new string[airports.Count];
            for (int i = 0; i < airports.Count; i++)
            {
                airportstring[i] = (airports[i].ToString());
            }
            return airportstring;
        }
            
            
    }
}