using Bot.Objects;
using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
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
        public abstract Dictionary<string, State> Children { get; }
        public abstract State Child { get; }
    }
}
