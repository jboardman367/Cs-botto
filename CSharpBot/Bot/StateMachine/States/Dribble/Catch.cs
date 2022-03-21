using Bot.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine.States.Dribble
{
    public class Catch : State
    {
        public override void Enter()
        {
            Console.WriteLine("Entering state dribble.catch");
        }
        public override void Exit()
        {
            throw new NotImplementedException();
        }
        public override void Step()
        {
            var me = GameState.Me;

            if (me.IsGrounded)
            {

            }
            else
            {
                // try to land nicely
                bool impactsRoof = me.Location.Z + me.Velocity.Z * me.Velocity.Z / 1300 > 2044 - 17.05f;
                if (impactsRoof)
                {

                }
            }
        }
    }
}
