using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.controller;
using Tp_02_02.model.States;

namespace Tp_02_02.model
{
    public class Scenario
    {
        public List<Airport> AirportList { get; set; }
        private State state;
        public int speed = 1000;

        public Scenario()
        {
            state = new ReadyState(this);
            AirportList = new List<Airport>();
        }

        public Scenario PerformOperations() {
            // feux, observer et secours sont des clients injecter de facon aleatoire sur la map
            // TODO feux aleatoire (1 a 2 par heure)
            // TODO secours aleatoire (1 a 3 par heure)
            // TODO observer aleatoire (1 par heure)


            return this;
        }

        public State GetState() { return state; }

        public void changeState(State state)
        {
            this.state = state;
            Console.WriteLine($"State: {state}");
        }

        // Facade
        public void Play() { state.PlayStop(); }

        // Facade
        public void Forward() { state.Forward(); }
    }
}
