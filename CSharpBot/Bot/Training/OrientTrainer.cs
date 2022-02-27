using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using RLBotDotNet.GameState;
using System.Drawing;
using Bot.Controllers.Air;
using MyGameState = Bot.Game.GameState;
using RLBotDotNet;

namespace Bot.Training
{
    public class OrientTrainer : ITraining
    {
        int stepCount;
        Vector3 pointAt;
        Vector3 orientTo;
        bool hasStateSet = false;
        Random random = new Random();
        Bot bot;
        static Vector3 center = new Vector3(0, 0, 1000);
        public OrientTrainer(Bot bot)
        {
            this.bot = bot;
            stepCount = 0;
            pointAt = Vector3.Normalize(new Vector3((float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f)) * 500;
            orientTo = Vector3.Normalize(new Vector3((float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f)) * 200;
        }
        public void Step()
        {
            if (!hasStateSet)
            {
                GameState gameState = new GameState();
                CarState carState = new CarState() { PhysicsState = new PhysicsState() { Location = new DesiredVector3(0, 0, 1000), Velocity = new DesiredVector3(0, 0, 0)} };
                gameState.SetCarState(0, carState);
                gameState.GameInfoState = new GameInfoState() { WorldGravityZ = 0.001f };
                bot.SetGameState(gameState);
                hasStateSet = true;
            }

            stepCount++;

            if (stepCount % 400 == 0)
            {
                pointAt = Vector3.Normalize(new Vector3((float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f)) * 500;
                orientTo = Vector3.Normalize(new Vector3((float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f)) * 200;
            }

            bot.Renderer.DrawLine3D(Color.Red, center, center + orientTo);
            bot.Renderer.DrawLine3D(Color.Aqua, center, center + pointAt);
            bot.RenderSphere(Color.Aqua, new Vector3(0, 0, 100), 90.75f);
            Controller cnt = Reorient.GetController(MyGameState.Me, pointAt, orientTo);
            Bot.Controller = cnt;

            // lockstep abuse
        }
    }
}
