using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Game;
using RLBotDotNet;
using Bot.StateMachine;
using System.Numerics;

namespace Bot.StateMachine.States.Kickoff
{
    public class Kickoff : State
    {
        public Kickoff()
        {
            Children.Add("waitForKickoffPause", new WaitForKickoffPause());
            Children.Add("speedflip", new Speedflip());
            Children.Add("fifty", new Fifty());
        }
        public override void Enter()
        {
            Console.WriteLine("Entering state kickoff");
            hasBeenKickoffPause = false;
        }

        public override void Exit()
        {

        }

        public override void Step()
        {
            hasBeenKickoffPause |= GameState.IsKickoffPause;
            if (hasBeenKickoffPause && (GameState.Ball.Location - Vector3.UnitY * 92.75f).Length() > 300)
            {
                EventHandler.OnKickoffComplete();
            }
        }

        bool hasBeenKickoffPause;
    }

    
}
