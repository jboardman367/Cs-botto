using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;

namespace Bot.StateMachine.States.Kickoff
{
    public class WaitForKickoffPause : State
    {
        int stepCounter;
        public override void Enter()
        {
            stepCounter = 0;
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Step()
        {
            // Wiggles!
            Controller controller = Bot.Controller;
            controller.Steer = MathF.Sin(stepCounter / 50f);

            // Call OnKickoffPauseStart if necessary
            if (packet.GameInfo.IsKickoffPause)
            {
                EventHandler.OnKickoffPauseStart();
            }
        }

        Dictionary<string, State> children = new Dictionary<string, State>();
        public override Dictionary<string, State> Children { get { return children; } }

        State child = null;
        public override State Child { get { return child; } }
    }
}
