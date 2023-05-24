namespace Tp_02.model.Aircrafts.SpecialAircraft
{
    /// <summary>
    ///  Aircraft de type Tank
    /// </summary>
    [Serializable]
    public class TankAircraft : SpecialAircraft
    {
        public bool IsMissionDone = false; // si la mission de l'aircraft est termine
        public bool IsFull = true; // si  l'aircraft est remplis

        public TankAircraft() { }
    }
}
