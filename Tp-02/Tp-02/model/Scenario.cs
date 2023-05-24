namespace Tp_02.model
{
    /// <summary>
    /// Classe scenario contenant tout les aeroport et les avions 
    /// </summary>
    [Serializable]
    public class Scenario
    {
        public List<Airport> AirportList { get; set; }

        public Scenario()
        {
            AirportList = new List<Airport>();
        }
    }
}
