namespace Tp_02_02.model.Aircrafts.States
{
    /// <summary>
    /// etat de l'avion en attente
    /// </summary>
    public class WaitingState : AircraftState
    {
        public WaitingState(Aircraft aircraft)
        {
            this.aircraft = aircraft;
        }
    }
}
