using Bot.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine
{
    public static partial class EventHandler
    {
        public static void OnKickoffPauseStart()
        {
            Console.WriteLine("OnKickoffPauseStart triggered");
            Console.WriteLine(GameState.Allies.Length.ToString() + GameState.Me.Team.ToString());
            switch (GameState.Allies.Length)
            {
                case 0:
                    StateMachine.ChangeState("kickoff.speedflip");
                    break;
            }
        }
    }
}
