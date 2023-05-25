using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.controller;
using Tp_02_02.model.Clients;
using Tp_02_02.model.Clients.SpecialClients;
using Tp_02_02.model.States;
using System.Numerics;

namespace Tp_02_02.model
{
    public class Scenario
    {
        public List<Airport> AirportList { get; set; }
        public List<SpecialClient> SpecialClientList { get; set; }
        private State state;
        public int speed { get; set; }
        public double time { get; set; } // in seconds

        public Scenario()
        {
            speed = 1000;
            time = 0;
            state = new ReadyState(this);
            AirportList = new List<Airport>();
        }

        public Scenario PerformOperations() {
            for (int i = 0; i < AirportList.Count; i++)
            {
                AirportList = AirportList[i].RunAirport(AirportList);
            }

            InjectSpecialClients(); 
            InjectTransportClients();

            return this;
        }

        private void AssignEmergencyClients(SpecialClient emergencyClient)
        {
            // TODO check nearest airport to the emergency
            // TODO add emergency client to that airport
        }

        private void InjectSpecialClients()
        {
            ClientFactory clientFactory = new ClientFactory();
            if(time % 1800 == 0)
            {
                SpecialClient fireClient = clientFactory.CreateSpecialClientWithRandomPos("Fire");
                SpecialClientList.Add(fireClient);
                AssignEmergencyClients(fireClient);
            }

            if (time % 3600 == 0)
            {
                SpecialClient rescueClient = clientFactory.CreateSpecialClientWithRandomPos("Rescue");
                SpecialClientList.Add(rescueClient);
                AssignEmergencyClients(rescueClient);
            }

            if (time%1200== 0)
            {
                SpecialClient observerClient = clientFactory.CreateSpecialClientWithRandomPos("Observer");
                SpecialClientList.Add(observerClient);
            }

        }

        private void InjectTransportClients()
        {
            if (time % 3600 == 0)
            {
                for (int i = 0; i < AirportList.Count; i++)
                {
                    AirportList[i].InjectClients(AirportList);
                }
            }
        }

        public string giveMeTheTime()
        {
            double f = time;
            TimeSpan t = TimeSpan.FromSeconds(f);
            return string.Format("{0}:{1}:{2}:{3}", ((int)t.TotalHours), t.Minutes, t.Seconds,t.Milliseconds);
        }

        public State GetState() { return state; }

        public void changeState(State state)
        {
            this.state = state;
        }

        // Facade
        public void Play() { state.PlayStop(); }

        // Facade
        public void Forward() { state.Forward(); }
    }
}
