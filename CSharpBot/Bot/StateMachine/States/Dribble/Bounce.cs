using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine.States.Dribble
{
    public class Bounce : State
    {
        public override void Enter()
        {
            Console.WriteLine("Entering state dribble.bounce");
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Step()
        {
            throw new NotImplementedException();
        }
    }
}
