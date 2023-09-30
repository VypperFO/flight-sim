namespace Tp_02_02.model.States
{
    /// <summary>
    /// classe d'etat du scenario qui arretter
    /// </summary>
    public class ReadyState : State
    {
        public ReadyState(Scenario scenario)
        {
            this.scenario = scenario;
        }

        /// <summary>
        /// change l'etat du scenario pour arret
        /// </summary>
        public override void PlayStop()
        {
            scenario.changeState(new PlayingState(scenario));
        }

        public override void Forward()
        {
            throw new NotImplementedException();
        }
    }
}
