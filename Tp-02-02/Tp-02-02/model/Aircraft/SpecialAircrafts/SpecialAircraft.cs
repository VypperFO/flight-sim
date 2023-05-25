using System.Numerics;
using Tp_02_02.model.Aircrafts.States;

namespace Tp_02_02.model.Aircrafts.SpecialAircrafts
{
    public class SpecialAircraft : Aircraft
    {
        public override void GoTo(Vector2 targetPosition)
        {
            float distance = Vector2.Distance(CurrentPosition, targetPosition);
            float step = speed / distance;

            CurrentPosition = Vector2.Lerp(CurrentPosition, targetPosition, step);

            if (Vector2WithinError(CurrentPosition, targetPosition, 10))
            {
                Destination = StartingPosition;
                if (Vector2WithinError(CurrentPosition, StartingPosition, 5))
                {
                    changeState(new WaitingState(this));
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
