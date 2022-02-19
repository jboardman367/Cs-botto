using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace Bot.Game.Ball
{
    public struct BallPrediction
    {
        public BallPrediction(rlbot.flat.BallPrediction flatPrediction, bool inverted)
        {
            Slices = new BallSlice?[flatPrediction.SlicesLength];

            GoalConcedeSlice = null;
            GoalScoreSlice = null;

            for (int i = 0; i < flatPrediction.SlicesLength; i++)
            {
                rlbot.flat.PredictionSlice? slice = flatPrediction.Slices(i);
                if (slice != null)
                {
                    BallSlice processed = new BallSlice((rlbot.flat.PredictionSlice)slice, inverted);
                    Slices[i] = processed;
                    if (processed.Location.Y > 5235)
                    {
                        GoalScoreSlice = Slices[i];
                    }
                    else if (processed.Location.Y < -5235)
                    {
                        GoalConcedeSlice = Slices[i];
                    }

                }
                else
                {
                    Slices[i] = null;
                }
            }
        }

        public BallSlice?[] Slices { get; private set; }
        public BallSlice? GoalScoreSlice { get; private set; }
        public BallSlice? GoalConcedeSlice { get; private set; }
    }


    public struct BallSlice
    {
        public Vector3 Location { get; private set; }
        public Vector3 Velocity { get; private set; }
        public float Time { get; private set; }
        public BallSlice(rlbot.flat.PredictionSlice slice, bool inverted)
        {
            var physics = slice.Physics.Value;
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
            Time = slice.GameSeconds;
        }
    }
}
