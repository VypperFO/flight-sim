using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.model;

namespace Tp_02_02.controller.States
{
    public class ReadyState: State
    {
        public ReadyState(COTAI sim) {
            this.sim = sim;
        }

        public override void Load(string filePath)
        {
            sim.changeState(new UnloadedState(sim));
        }
        public override void Play()
        {
            sim.changeState(new PlayingState(sim));
            Console.WriteLine("Play clicked");
        }

        public override void Forward()
        {
            // TOTO pop up that says you have to click play
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            // TOTO pop up that says you have to click play
            throw new NotImplementedException();
        }
    }
}
