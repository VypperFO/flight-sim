namespace Tp_02_02.model.Aircrafts.States
{
    public abstract class AircraftState
    {
        protected Aircraft aircraft;
        public AircraftState() { }
        public AircraftState(Aircraft aircraft)
        {
            this.aircraft = aircraft;
        }
    }
}
