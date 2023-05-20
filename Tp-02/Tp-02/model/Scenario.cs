namespace Tp_02.model
{
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
