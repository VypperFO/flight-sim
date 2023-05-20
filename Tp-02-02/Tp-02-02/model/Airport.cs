﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tp_02_02.model.Aircrafts;
using Tp_02_02.model.Aircrafts.SpecialAircrafts;
using Tp_02_02.model.Aircrafts.TransportAircrafts;

namespace Tp_02_02.model
{
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

        public Airport(string name, string coords, int minPassenger, int maxPassenger, int minMerchandise, int maxMerchandise)
        {
            Name = name;
            Coords = coords;
            MinPassenger = minPassenger;
            MaxPassenger = maxPassenger;
            MinMerchandise = minMerchandise;
            MaxMerchandise = maxMerchandise;
            AircraftList = new List<Aircraft>();
        }
    }
}
