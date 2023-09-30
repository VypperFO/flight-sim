using System.Numerics;
using Tp_02_02.model.Aircrafts.States;

namespace Tp_02_02.model.Aircrafts
{
    /// <summary>
    /// Classe avion
    /// </summary>
    public class Aircraft
    {
        protected AircraftState state; // etat de l'avion
        public Vector2 CurrentPosition { get; set; } // position en se moment de l'avion
        public Vector2 StartingPosition { get; set; } // position de depart de l'avion
        public Vector2 Destination { get; set; } // destination de l'avion
        public string Name { get; set; } // nom de l'avion
        public int Capacity { get; set; } // capaciter de l'avion
        public int speed { get; set; } // vitesse de l'avion

        /// <summary>
        /// initialise l'avion avec sa vitesse de base et sont etat de base
        /// </summary>
        public Aircraft()
        {
            speed = 15;
            state = new WaitingState(this);
        }

        /// <summary>
        /// envoie l'avion vers la destination donner en paramette
        /// </summary>
        /// <param name="targetPosition"></param>
        public virtual void GoTo(Vector2 targetPosition)
        {
            float distance = Vector2.Distance(CurrentPosition, targetPosition);
            float step = speed / distance;

            CurrentPosition = Vector2.Lerp(CurrentPosition, targetPosition, step);

            if (Vector2WithinError(CurrentPosition, targetPosition, 10))
            {
                changeState(new WaitingState(this));
            }
        }

        #region Utils

        /// <summary>
        /// retourne si l'avion est a la destination avec une marge d'erreur
        /// </summary>
        /// <param name="vector1">position 1</param>
        /// <param name="vector2">position 2</param>
        /// <param name="error">marge d'erreur</param>
        /// <returns>si l'avion est dans la marge derreur</returns>
        protected static bool Vector2WithinError(Vector2 vector1, Vector2 vector2, float error)
        {
            float deltaX = Math.Abs(vector1.X - vector2.X);
            float deltaY = Math.Abs(vector1.Y - vector2.Y);

            return deltaX <= error && deltaY <= error;
        }

        /// <summary>
        /// donne l'etat de l'avion
        /// </summary>
        /// <returns> l'etat de l'avion</returns>
        public AircraftState GetState()
        {
            return state;
        }

        /// <summary>
        /// change l'etat de l'avion
        /// </summary>
        /// <param name="state">l'etat de l'avion voulue</param>
        public void changeState(AircraftState state)
        {
            this.state = state;
        }

        /// <summary>
        /// donne le nom de l'avion sous forme de string
        /// </summary>
        /// <returns>le nom de l'avion sous forme de string</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
