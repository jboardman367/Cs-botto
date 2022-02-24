using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Game;
using RLBotDotNet;
using Bot.StateMachine;

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
            
        }

        public override void Exit()
        {

        }

        public override void Step()
        {
            
        }

        
    }

    
}
