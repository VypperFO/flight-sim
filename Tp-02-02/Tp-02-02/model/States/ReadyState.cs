namespace Tp_02_02.model.States
{
    public class ReadyState : State
    {
        public ReadyState(Scenario scenario)
        {
            this.scenario = scenario;
        }

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
