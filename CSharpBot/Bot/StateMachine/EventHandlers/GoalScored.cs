﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine
{
    public static partial class EventHandler
    {
        public static void OnGoalScored()
        {
            Console.WriteLine("OnGoalScored triggered");
            StateMachine.ChangeState("kickoff.waitForKickoffPause");
        }
    }
}
