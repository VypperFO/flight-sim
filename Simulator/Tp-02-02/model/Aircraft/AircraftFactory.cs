using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.TransportAircrafts;

namespace Tp_02_02.model.Aircrafts
{
    /// <summary>
    /// Factory d'aircraft permmetant de creer des aircraft facilement
    /// </summary>
    public class AircraftFactory
    {
        private static AircraftFactory? factory; // factory de la classe

        private AircraftFactory()
        {
            // private constructor to prevent direct instantiation
        }

        /// <summary>
        /// singleton factory
        /// </summary>
        public static AircraftFactory GetAircraftFactory
        {
            get
            {
                factory ??= new AircraftFactory();

                return factory;
            }
        }

        /// <summary>
        /// creer une avion avec le type donner en paramettre
        /// </summary>
        /// <param name="type">type de l'avion voulue</param>
        /// <returns>l'avion de type voulue</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Aircraft CreateAircraft(string type)
        {
            switch (type)
            {
                case "passenger":
                    return new PassengerAircraft();
                case "cargo":
                    return new CargoAircraft();
                case "tank":
                    return new TankAircraft();
                case "observer":
                    return new ObserverAircraft();
                case "helicopter":
                    return new HelicopterAircraft();
                default:
                    throw new NotSupportedException($"Aircraft type {type} is not supported.");
            }
        }
    }
}
