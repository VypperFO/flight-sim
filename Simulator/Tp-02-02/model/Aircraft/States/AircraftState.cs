namespace Tp_02_02.model.Aircrafts.States
{
    /// <summary>
    /// classe etat d'avion
    /// </summary>
    public abstract class AircraftState
    {
        protected Aircraft aircraft; // avion a qui l'etat va
        public AircraftState() { }
        public AircraftState(Aircraft aircraft)
        {
            this.aircraft = aircraft;
        }
    }
}
