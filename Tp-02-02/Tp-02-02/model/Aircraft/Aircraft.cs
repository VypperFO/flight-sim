using System.Numerics;
using Tp_02_02.model.Aircrafts.States;

namespace Tp_02_02.model.Aircrafts
{
    public class Aircraft
    {
        protected AircraftState state;
        protected Vector2 CurrentPosition;
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Aircraft() {
            state = new WaitingState(this);
        }

        public void changeState(AircraftState state)
        {
            this.state = state;
        }

        public void Fly()
        {
            state.Fly();
        }

        public void Wait()
        {
            state.Wait();
        }

        public void DoMaintenance()
        {
            state.DoMaintenance();
        }
    }
}
