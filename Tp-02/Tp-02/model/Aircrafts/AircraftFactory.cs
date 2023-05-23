using Tp_02.model.Aircrafts.SpecialAircraft;
using Tp_02.model.Aircrafts.TransportAircraft;


namespace Tp_02.model.Aircrafts
{
    public class AircraftFactory
    {
        private static AircraftFactory? factory;

        private AircraftFactory()
        {
        }

        public static AircraftFactory GetAircraftFactory
        {
            get
            {
                factory ??= new AircraftFactory();

                return factory;
            }
        }

        public Aircraft CreateAircraft(string type, string airportGPS)
        {
            switch (type)
            {
                case "Passager":
                    return new PassengerAircraft();
                case "Cargo":
                    return new CargoAircraft();
                case "Citerne":
                    TankAircraft tank = new()
                    {
                        DepartureAirport = airportGPS
                    };
                    return tank;
                case "Observateur":
                    ObserverAircraft observer = new()
                    {
                        DepartureAirport = airportGPS
                    };
                    return observer;
                case "Helicoptère":
                    HelicopterAircraft heli = new()
                    {
                        DepartureAirport = airportGPS
                    };
                    return heli;
                default:
                    throw new NotSupportedException($"Aircraft type {type} is not supported.");
            }
        }
    }
}
