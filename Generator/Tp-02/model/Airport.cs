using System.Xml.Serialization;
using Tp_02.model.Aircrafts;
using Tp_02.model.Aircrafts.SpecialAircraft;
using Tp_02.model.Aircrafts.TransportAircraft;

namespace Tp_02.model
{
    /// <summary>
    /// Classe airport permmetant de creer des aeroport d'avion
    /// </summary>
    [Serializable]
    public class Airport
    {
        public string Name { get; set; }
        public string Coords { get; set; }
        public int MinPassenger { get; set; }
        public int MaxPassenger { get; set; }
        public int MinMerchandise { get; set; }
        public int MaxMerchandise { get; set; }
        [XmlArray("AircraftList")]
        [XmlArrayItem("HelicopterAircraft", typeof(HelicopterAircraft))]
        [XmlArrayItem("ObserverAircraft", typeof(ObserverAircraft))]
        [XmlArrayItem("TankAircraft", typeof(TankAircraft))]
        [XmlArrayItem("CargoAircraft", typeof(CargoAircraft))]
        [XmlArrayItem("PassengerAircraft", typeof(PassengerAircraft))]
        [XmlArrayItem("Aircraft", typeof(Aircraft))]
        public List<Aircraft> AircraftList { get; set; }

        public Airport()
        {
            AircraftList = new List<Aircraft>();
        }
    }
}
