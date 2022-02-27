using System;
using System.Drawing;
using System.Numerics;
using Bot.Game;
using Bot.Training;
using RLBotDotNet;

namespace Bot
{
    // We want to our bot to derive from Bot, and then implement its abstract methods.
    public class Bot : RLBotDotNet.Bot
    {
        // We want the constructor for our Bot to extend from RLBotDotNet.Bot, but we don't want to add anything to it.
        // You might want to add logging initialisation or other types of setup up here before the bot starts.
        public Bot(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex) 
        {
            StateMachine.StateMachine.ChangeState("kickoff.waitForKickoffPause");
            training = new OrientTrainer(this);
        }

        public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
        {
            GameState.Update(gameTickPacket, GetFieldInfo(), GetBallPrediction(), Index);

            if (training != null)
            {
                training.Step();
            }
            else
            {
                StateMachine.StateMachine.Step();
            }

            return controller;
        }

        // Static fields for states and handlers to access
        static Controller controller = new Controller();
        public static Controller Controller { set { controller = value; } }

        public new void SetGameState(RLBotDotNet.GameState.GameState gameState) => base.SetGameState(gameState);

        ITraining training;

        public void RenderSphere(Color color, Vector3 center, float radius)
        {
            Vector3[] points = new Vector3[100];
            for (int i = 0; i < 100; i++)
            {
                float t = i / 100 * 2 - 1;
                float r = radius * MathF.Sqrt(1 - t * t);
                float theta = 0.62f * i;
                points[i] = new Vector3(center.X + r * MathF.Cos(theta), center.Y + r * MathF.Sin(theta), center.Z + t * radius);
            }
            // Console.WriteLine(points[0].ToString() + " " + points[17].ToString() + " " + points[48].ToString());
            Renderer.DrawPolyLine3D(color, points);
        }
    }

}