using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.TransportAircrafts;

namespace Tp_02_02.model.Aircrafts
{
    public class AircraftFactory
    {
        private static AircraftFactory? factory;

        private AircraftFactory()
        {
            // Private constructor to prevent direct instantiation
        }

        public static AircraftFactory GetAircraftFactory
        {
            get
            {
                factory ??= new AircraftFactory();

                return factory;
            }
        }

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
