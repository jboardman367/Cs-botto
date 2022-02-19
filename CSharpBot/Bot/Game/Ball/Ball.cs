using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bot.Game.Ball
{
    public class Ball
    {
        public Ball()
        {

        }

        public void Update(rlbot.flat.GameTickPacket packet, rlbot.flat.BallPrediction prediction, bool inverted)
        {
            LastTouch = new Touch(packet.Ball.Value.LatestTouch.Value, packet.GameInfo.Value.SecondsElapsed, inverted);
            var physics = packet.Ball.Value.Physics.Value;
            if (inverted)
            {
                Location = new Vector3(-physics.Location.Value.X, -physics.Location.Value.Y, physics.Location.Value.Z);
                Velocity = new Vector3(-physics.Velocity.Value.X, -physics.Velocity.Value.Y, physics.Velocity.Value.Z);
            }
            else
            {
                Location = new Vector3(physics.Location.Value.X, physics.Location.Value.Y, physics.Location.Value.Z);
                Velocity = new Vector3(physics.Velocity.Value.X, physics.Velocity.Value.Y, physics.Velocity.Value.Z);
            }
            Prediction = new BallPrediction(prediction, inverted);
        }

        public Touch LastTouch { get; private set; }
        public Vector3 Location { get; private set; }
        public Vector3 Velocity { get; private set; }

        public BallPrediction Prediction { get; private set; }
    }

    public struct Touch
    {
        public Vector3 Location { get; private set; }
        public float TimeSinceTouch { get; private set; }
        public Car Car { get; private set; }
        public Vector3 Normal { get; private set; }
        public Touch(rlbot.flat.Touch touch, float currentGameSeconds, bool inverted)
        {
            if (inverted)
            {
                Location = new Vector3(-touch.Location.Value.X, -touch.Location.Value.Y, touch.Location.Value.Z);
                TimeSinceTouch = currentGameSeconds - touch.GameSeconds;
                Car = GameState.GetCarByIndex(touch.PlayerIndex);
                Normal = new Vector3(-touch.Normal.Value.X, -touch.Normal.Value.Y, touch.Normal.Value.Z);
            }
            else
            {
                Location = new Vector3(touch.Location.Value.X, touch.Location.Value.Y, touch.Location.Value.Z);
                TimeSinceTouch = currentGameSeconds - touch.GameSeconds;
                Car = GameState.GetCarByIndex(touch.PlayerIndex);
                Normal = new Vector3(touch.Normal.Value.X, touch.Normal.Value.Y, touch.Normal.Value.Z);
            }
        }
    }
}
