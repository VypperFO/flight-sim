using Tp_02_02.controller;

namespace Tp_02_02.model.States
{
    public class PlayingState : State
    {
        public PlayingState(Scenario scenario)
        {
            this.scenario = scenario;
        }

        public override void PlayStop()
        {
            scenario.changeState(new ReadyState(scenario));
        }

        public override void Forward()
        {
            scenario.speed = scenario.speed / 2;
            Console.WriteLine("Forward clicked");
        }
    }
}
