using System.Numerics;
using Tp_02_02.model.Aircrafts.States;
using Tp_02_02.model.Clients.SpecialClients;

namespace Tp_02_02.model.Aircrafts
{
    public class Aircraft
    {
        protected AircraftState state;
        public Vector2 CurrentPosition;
        public Vector2 StartingPosition;
        public Vector2 Destination;
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int speed { get; set; }

        public Aircraft() {
            speed = 20;
            state = new WaitingState(this);
        }

        public void GoTo(Vector2 targetPosition)
        {
            Console.WriteLine($"x: {CurrentPosition.X}, y: {CurrentPosition.Y}");

            float distance = Vector2.Distance(CurrentPosition, targetPosition);
            float step = speed / distance;

            CurrentPosition = Vector2.Lerp(CurrentPosition, targetPosition, step);

            if (Vector2WithinError(CurrentPosition, targetPosition, 5))
            {
                changeState(new MaintenanceState(this));
                Console.WriteLine($"State changed: {GetState}");
            }
        }

        public bool Vector2WithinError(Vector2 vector1, Vector2 vector2, float error)
        {
            float deltaX = Math.Abs(vector1.X - vector2.X);
            float deltaY = Math.Abs(vector1.Y - vector2.Y);

            return deltaX <= error && deltaY <= error;
        }

        public AircraftState GetState()
        {
            return state;
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

    

        public override string ToString()
        {
            return Name;
        }
    }
}
