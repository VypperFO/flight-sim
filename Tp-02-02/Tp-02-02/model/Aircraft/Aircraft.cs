using Tp_02_02.model.Aircrafts.States;

namespace Tp_02_02.model.Aircrafts
{
    public class Aircraft
    {
        protected int MaintenanceTime;
        protected State CurrentState;
        protected Position CurrentPosition;
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Aircraft() { }
    }
}
