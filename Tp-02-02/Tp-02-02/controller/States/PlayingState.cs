namespace Tp_02_02.controller.States
{
    public class PlayingState : State
    {
        public PlayingState(COTAI sim)
        {
            this.sim = sim;
        }

        public override void Load(string filePath)
        {
            sim.changeState(new UnloadedState(sim));
        }
        public override void Forward()
        {
            Console.WriteLine("Forward clicked");
        }

        public override void Play()
        {
            Console.WriteLine("Play clicked");
        }

        public override void Stop()
        {
            Console.WriteLine("Stop clicked");
            sim.changeState(new ReadyState(sim));
        }
    }
}
