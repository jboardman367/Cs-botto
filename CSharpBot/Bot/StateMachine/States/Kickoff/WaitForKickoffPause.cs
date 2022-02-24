using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLBotDotNet;
using Bot.Game;

namespace Bot.StateMachine.States.Kickoff
{
    public class WaitForKickoffPause : State
    {
        int stepCounter;
        public override void Enter()
        {
            Console.WriteLine("Entering state kickoff.waitForKickoffPause");
            stepCounter = 0;
        }

        public override void Exit()
        {

        }

        public override void Step()
        {
            // Wiggles!
            float steer =  MathF.Sin(stepCounter / 15f);
            stepCounter++;

            // Call OnKickoffPauseStart if necessary
            if (GameState.IsKickoffPause)
            {
                EventHandler.OnKickoffPauseStart();
            }
            Bot.Controller = new Controller() { Steer = steer };
        }

        
    }
}
