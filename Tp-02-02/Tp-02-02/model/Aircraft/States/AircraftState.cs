using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_02_02.model.Aircrafts.States
{
    public abstract class AircraftState
    {
        protected Aircraft aircraft;
        public AircraftState() { }
        public AircraftState(Aircraft aircraft) 
        {
            this.aircraft = aircraft;
        }

        public abstract void Fly();
        public abstract void Wait();
        public abstract void DoMaintenance();
    }
}
