using Bot.Controllers.Air;
using Bot.Game;
using RLBotDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bot.StateMachine.States.Kickoff
{
    public class Fifty : State
    {
        public override void Enter()
        {
            Console.WriteLine("Entering state kickoff.fifty");
            hasLanded = false;
        }

        public override void Exit()
        {
        }

        public override void Step()
        {
            var me = GameState.Me;
            var ball = GameState.Ball;

            if (me.IsGrounded)
            {
                // Handle pointing at the ball
                hasLanded = true;
                Vector3 localBall = Orientation.RelativeFrom(ball.Location - me.Location, me.Orientation);
                Controller controller = new Controller() {
                    Throttle = 1,
                    Steer = Math.Clamp(3 * MathF.Atan2(localBall.Y, localBall.X), -1f, 1f),
                    Boost = me.Velocity.Length() < 2290
                };

                // TODO: snipe corners against slow kickoffs

                // Jump for the flip
                controller.Jump = (localBall.Length() - 72.96f - 92.75f) / me.Velocity.Length() < 0.26;

                Bot.Controller = controller;
            } 
            else if (hasLanded)
            {
                Vector3 localBall = Orientation.RelativeFrom(ball.Location - me.Location, me.Orientation);
                Controller controller = new();
                if ((localBall.Length() - 72.96f - 92.75f) / me.Velocity.Length() < 0.1)
                {
                    if (me.FlipTimer > 0)
                    {
                        // Do the flip
                        controller.Jump = true;
                        controller.Pitch = -1;
                        controller.Yaw = MathF.CopySign(0.7f, localBall.Y);
                    }
                    else
                    {
                        // Release jump so it is possible to flip
                        controller.Pitch = -1;
                        controller.Yaw = MathF.CopySign(0.7f, localBall.Y);
                    }
                }
                else
                {
                    controller.Jump = true;
                }
                Bot.Controller = controller;
            } 
            else
            {
                // Land with maximum speed
                Controller controller = Reorient.GetController(me, me.Velocity, Vector3.UnitZ);
                controller.Boost = me.Velocity.Length() < 2290;
                controller.Handbrake = true;
                controller.Throttle = 1;
                Bot.Controller = controller;
            }
        }

        bool hasLanded;
    }
}
