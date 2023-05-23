using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void PerformOperations() {
            Thread.Sleep(speed);

            if(state is PlayingState)
            {
                Console.WriteLine("cum");
                PerformOperations();
            }  
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
