namespace Tp_02_02.model.States
{
    /// <summary>
    /// Classe abstraite d'etat
    /// </summary>
    public abstract class State
    {
        protected Scenario scenario; // scenario du simulateur

        public State() { }
        public State(Scenario scenario)
        {
            this.scenario = scenario;
        }

        public abstract void PlayStop();
        public abstract void Forward();
    }
}
