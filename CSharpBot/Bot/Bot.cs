using System.Drawing;
using System.Numerics;
using Bot.Game;
using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;

namespace Bot
{
    // We want to our bot to derive from Bot, and then implement its abstract methods.
    class Bot : RLBotDotNet.Bot
    {
        // We want the constructor for our Bot to extend from RLBotDotNet.Bot, but we don't want to add anything to it.
        // You might want to add logging initialisation or other types of setup up here before the bot starts.
        public Bot(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex) 
        {
            StateMachine.StateMachine.ChangeState("kickoff.waitForKickoffPause");
        }

        public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
        {
            GameState.Update(gameTickPacket, GetFieldInfo(), GetBallPrediction(), Index);

            StateMachine.StateMachine.Step();

            return controller;
        }

        // Static fields for states and handlers to access
        static Controller controller = new Controller();
        public static Controller Controller { get { return controller; } }
    }
}