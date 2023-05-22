using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.controller;
using Tp_02_02.model;

namespace Tp_02_02.model.States
{
    public abstract class State
    {
        protected Scenario scenario;

        public State() { }
        public State(Scenario scenario)
        {
            this.scenario = scenario;
        }

        public abstract void PlayStop();
        public abstract void Forward();
    }
}
