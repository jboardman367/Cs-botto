﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bot.Objects;
using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;

namespace Bot.StateMachine.States.Kickoff
{
    public class Kickoff : State
    {
        public Kickoff()
        {
            children.Add("waitForKickoffPause", new WaitForKickoffPause());
        }
        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Step()
        {

        }

        Dictionary<string, State> children = new Dictionary<string, State>();
        public override Dictionary<string, State> Children { get { return children; } }

        State child = null;
        public override State Child { get { return child; } }
    }
}
