namespace Tp_02_02.model.Aircrafts.States
{
    /// <summary>
    /// etat de l'avion en vol
    /// </summary>
    public class FlyingState : AircraftState
    {
        public FlyingState(Aircraft aircraft)
        {
            this.aircraft = aircraft;
        }
    }
}
