using Bot.Game;
using Bot.Controllers.Air;
using RLBotDotNet;
using System;
using System.Numerics;
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
                if (MathF.Abs(me.Location.Y) < 5120)
                {  // Not inside goal
                    bool impactsRoof = me.Location.Z + me.Velocity.Z * me.Velocity.Z / 1300 > 2044 - 17.05f;
                    if (impactsRoof)
                    {
                        Controller controller = Reorient.GetController(me, new Vector3(me.Velocity.X, me.Velocity.Y, 0), -Vector3.UnitZ);
                        Bot.Controller = controller;
                        return;
                    }
                    float timeToGround = (-me.Velocity.Z - MathF.Sqrt(me.Velocity.Z * me.Velocity.Z + 2 * 650 * (me.Location.Z - 17.05f))) / (2 * -650);
                    float timeToSideWall = (MathF.CopySign(4096 - 17.05f, me.Velocity.X) - me.Location.X) / me.Velocity.X;
                    float timeToBackWall = (MathF.CopySign(5120 - 19.05f, me.Velocity.Y) - me.Location.Y) / me.Velocity.Y;

                    // TODO: add in logic for corner landings
                    if (timeToSideWall < timeToBackWall && timeToSideWall < timeToGround)
                    {  // Side wall landing

                    }
                    else if (timeToBackWall < timeToGround)
                    {  // Back wall landing

                    }
                    else
                    {  // Ground landing

                    }
                }
            }
        }
    }
}
