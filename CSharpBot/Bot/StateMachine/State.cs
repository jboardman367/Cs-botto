using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine
{
    public abstract class State
    {
        public abstract void Step();
        public abstract void Enter();
        public abstract void Exit();
        public Dictionary<string, State> Children { get; } = new Dictionary<string, State>();
        public State Child { get; set; } = null;

    }
}
