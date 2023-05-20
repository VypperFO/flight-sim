using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Aircraft CreateAircraft(string type)
        {
            switch (type)
            {
                case "Passager":
                    return new PassengerAircraft();
                case "Cargo":
                    return new CargoAircraft();
                case "Citerne":
                    return new TankAircraft();
                case "Observateur":
                    return new ObserverAircraft();
                case "Helicoptère":
                    return new HelicopterAircraft();
                default:
                    throw new NotSupportedException($"Aircraft type {type} is not supported.");
            }
        }
    }
}
