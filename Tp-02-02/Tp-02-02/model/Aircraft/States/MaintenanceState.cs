namespace Tp_02_02.model.Aircrafts.States
{
    public class MaintenanceState : AircraftState
    {
        public int startTime { get; set; }
        public MaintenanceState(Aircraft aircraft)
        {
            this.aircraft = aircraft;
        }

        public override void DoMaintenance()
        {
            var th = new Thread(ExecuteInForeground);
            th.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Main thread ({0}) exiting...",
                              Thread.CurrentThread.ManagedThreadId);
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
