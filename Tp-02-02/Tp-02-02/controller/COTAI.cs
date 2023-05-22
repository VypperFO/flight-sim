using Tp_02_02.controller.States;
using Tp_02_02.model;

namespace Tp_02_02.controller
{
    public class COTAI
    {
        public FormSimulator simulatorForm;
        public Scenario scenario;
        private State state;

        [STAThread]
        static void Main()
        {
            COTAI controller = new COTAI();
        }

        public COTAI()
        {
            ApplicationConfiguration.Initialize();
            state = new UnloadedState(this);
            simulatorForm = new FormSimulator(this);
            Application.Run(simulatorForm);
        }

        public void changeState(State state)
        {
            this.state = state;
            Console.WriteLine($"State: {state}");
        }

        public void Load(string filePath)
        {
            changeState(new UnloadedState(this));
            state.Load(filePath);
        }

        public void Play()
        {
            state.Play();
        }

        public void Stop()
        {
            state.Stop();
        }

        public void Forward()
        {
            state.Forward();
        }
    }
}