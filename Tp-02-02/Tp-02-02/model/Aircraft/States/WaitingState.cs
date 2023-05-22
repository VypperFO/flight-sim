using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_02_02.model.Aircrafts.States
{
    public class WaitingState : AircraftState
    {
        public WaitingState(Aircraft aircraft)
        {
            this.aircraft = aircraft;
        }

        public override void DoMaintenance()
        {
            throw new NotImplementedException();
        }

        public override void Fly()
        {
            throw new NotImplementedException();
        }

        public override void Wait()
        {
            throw new NotImplementedException();
        }
    }
}
