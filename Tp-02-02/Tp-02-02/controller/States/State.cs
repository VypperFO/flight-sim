using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_02_02.model;

namespace Tp_02_02.controller.States
{
    public abstract class State
    {
        protected COTAI sim;
        
        public State() { }
        public State(COTAI sim) {
            this.sim = sim;
        }

        public abstract void Load(string filePath);
        public abstract void Play();
        public abstract void Stop();
        public abstract void Forward();
    }
}
