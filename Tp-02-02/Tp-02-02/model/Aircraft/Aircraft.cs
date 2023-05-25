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

        public virtual void GoTo(Vector2 targetPosition)
        {
            //Console.WriteLine($"Plane: {Name}, x: {CurrentPosition.X}, y: {CurrentPosition.Y}");

            float distance = Vector2.Distance(CurrentPosition, targetPosition);
            float step = speed / distance;

            CurrentPosition = Vector2.Lerp(CurrentPosition, targetPosition, step);

            if (Vector2WithinError(CurrentPosition, targetPosition, 10))
            {
                changeState(new WaitingState(this));
                Console.WriteLine($"{Name} changed state to {GetState().GetType().Name}");
            }
        }

        protected static bool Vector2WithinError(Vector2 vector1, Vector2 vector2, float error)
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

        public override string ToString()
        {
            return Name;
        }
    }
}
