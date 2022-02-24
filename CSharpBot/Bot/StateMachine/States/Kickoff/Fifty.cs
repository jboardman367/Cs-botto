using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine.States.Kickoff
{
    public class Fifty : State
    {
        public override void Enter()
        {
            Console.WriteLine("Entering state kickoff.fifty");
        }

        public override void Exit()
        {
        }

        public override void Step()
        {
        }
    }
}
