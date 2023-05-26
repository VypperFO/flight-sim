namespace Tp_02_02.model.States
{
    /// <summary>
    /// classe d'etat du scenario qui joue
    /// </summary>
    public class PlayingState : State
    {
        public PlayingState(Scenario scenario)
        {
            this.scenario = scenario;
        }

        /// <summary>
        /// change l'etat du scenario pour jouer
        /// </summary>
        public override void PlayStop()
        {
            scenario.changeState(new ReadyState(scenario));
        }

        /// <summary>
        /// augment la vitesse du scenario
        /// </summary>
        public override void Forward()
        {
            scenario.speed = scenario.speed / 2;
            Console.WriteLine("Forward clicked");
        }
    }
}
