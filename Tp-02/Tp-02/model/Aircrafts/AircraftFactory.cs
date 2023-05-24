using Tp_02.model.Aircrafts.SpecialAircraft;
using Tp_02.model.Aircrafts.TransportAircraft;


namespace Tp_02.model.Aircrafts
{
    /// <summary>
    /// Factory d'aircraft
    /// </summary>
    public class AircraftFactory
    {
        private static AircraftFactory? factory; 

        private AircraftFactory()
        {
        }

        /// <summary>
        /// initalise le factory et le retourne
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
        /// Creer un avions
        /// </summary>
        /// <param name="type">le type de l'avion</param>
        /// <param name="airportGPS">les coordonnes gps de sont aeroport</param>
        /// <returns>l'avion</returns>
        /// <exception cref="NotSupportedException">empeche les type inexistant d'avion</exception>
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
