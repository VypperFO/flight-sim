using System.Numerics;
using Tp_02_02.model.Aircrafts.States;

namespace Tp_02_02.model.Aircrafts.SpecialAircrafts
{
    /// <summary>
    /// classe de tout les avions special
    /// </summary>
    public class SpecialAircraft : Aircraft
    {
        /// <summary>
        /// Envoie l'avion dans une direction donner en paramettre
        /// </summary>
        /// <param name="targetPosition">la direction que lon veux envoyer l'avion</param>
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

        /// <summary>
        /// donne le nom de l'avion en string
        /// </summary>
        /// <returns>le nom de l'avion en string</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
