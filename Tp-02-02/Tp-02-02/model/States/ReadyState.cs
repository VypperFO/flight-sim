using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.controller;
using Tp_02_02.model;

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
            // TOTO pop up that says you have to click play
            throw new NotImplementedException();
        }
    }
}
