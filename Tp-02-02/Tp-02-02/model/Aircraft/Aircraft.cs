using System.Numerics;
using Tp_02_02.model.Aircrafts.States;

namespace Tp_02_02.model.Aircrafts
{
    public class Aircraft
    {
        protected AircraftState state;
        public Vector2 CurrentPosition;
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Aircraft() {
            CurrentPosition.X = 0;
            CurrentPosition.Y = 0;
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

        // A CORRIGER CAR IL NE RETOURNE PAS LE TYPE NIS LA CAPACITE CAR ON A PAS LE TYPE
        // DANS LES DONNE MEMBRE PIS LIVE JAI JUSTE BESOIN DU NOM SO SA VA RESTER DEMEME
        // JUSQUA TEMPS QUON A BESOIN DES AUTRE DONNE MEMBRE EN STRING SE QUI METTONERAIS
        // FORTEMENT JE PARLE DU OVVERIDE DU TOO STRING

        public override string ToString()
        {
            return Name;
        }
    }
}
