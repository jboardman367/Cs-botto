using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine.States.Dribble
{
    public class Dribble : State
    {
        public Dribble()
        {
            Children.Add("flick", new Flick());
            Children.Add("bounce", new Bounce());
            Children.Add("carry", new Carry());
            Children.Add("catch", new Catch());
            Children.Add("push", new Push());
        }
        public override void Enter()
        {
            Console.WriteLine("Entering state dribble");
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Step()
        {
            // Nothing to put here yet
        }
    }
}
