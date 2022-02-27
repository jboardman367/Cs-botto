using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bot.Game
{
    public struct Orientation
    {
        public float Pitch { get; private set; }
        public float Roll { get; private set; }
        public float Yaw { get; private set; }

        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }

        public Orientation(rlbot.flat.Rotator? rotator, bool inverted)
        {
            // The Rotator from the ball prediction is always null.
            // This ends up breaking this class unless we account for it.
            if (rotator.HasValue)
            {
                Pitch = rotator.Value.Pitch;
                Roll = rotator.Value.Roll;
                Yaw = inverted ? -rotator.Value.Yaw : rotator.Value.Yaw;
            }
            else
            {
                Pitch = 0;
                Roll = 0;
                Yaw = 0;
            }

            float cp = (float)Math.Cos(Pitch);
            float cy = (float)Math.Cos(Yaw);
            float cr = (float)Math.Cos(Roll);
            float sp = (float)Math.Sin(Pitch);
            float sy = (float)Math.Sin(Yaw);
            float sr = (float)Math.Sin(Roll);

            Forward = new Vector3(cp * cy, cp * sy, sp);
            Right = new Vector3(cy * sp * sr - cr * sy, sy * sp * sr + cr * cy, -cp * sr);
            Up = new Vector3(-cr * cy * sp - sr * sy, -cr * sy * sp + sr * cy, cp * cr);
        }

        public Orientation(Vector3 forward, float roll)
        {
            Roll = roll;
            Yaw = MathF.Atan2(forward.Y, forward.X);
            Pitch = MathF.Asin(forward.Z);

            float cp = (float)Math.Cos(Pitch);
            float cy = (float)Math.Cos(Yaw);
            float cr = (float)Math.Cos(Roll);
            float sp = (float)Math.Sin(Pitch);
            float sy = (float)Math.Sin(Yaw);
            float sr = (float)Math.Sin(Roll);

            Forward = new Vector3(cp * cy, cp * sy, sp);
            Right = new Vector3(cy * sp * sr - cr * sy, sy * sp * sr + cr * cy, -cp * sr);
            Up = new Vector3(-cr * cy * sp - sr * sy, -cr * sy * sp + sr * cy, cp * cr);
        }

        public static Vector3 RelativeFrom(Vector3 target, Orientation orientation)
        {
            float x = Vector3.Dot(target, orientation.Forward);
            float y = Vector3.Dot(target, orientation.Right);
            float z = Vector3.Dot(target, orientation.Up);

            return new Vector3(x, y, z);
        }
    }
}
