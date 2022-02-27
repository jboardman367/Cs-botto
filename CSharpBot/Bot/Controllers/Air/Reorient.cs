using RLBotDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Bot.Game;

namespace Bot.Controllers.Air
{
    public static class Reorient
    {
        public static Controller GetController(Car car, Vector3 targetForward, Vector3 targetUp)
        {
            targetForward = Vector3.Normalize(targetForward);
            targetUp = Vector3.Normalize(targetUp);
            Vector3 targetForwardLocal = Orientation.RelativeFrom(targetForward, car.Orientation);
            float pitchComponent = MathF.Atan2(targetForwardLocal.Z, targetForwardLocal.X);
            float yawComponent = MathF.Atan2(targetForwardLocal.Y, targetForwardLocal.X);

            Orientation end = new Orientation(targetForward, car.Orientation.Roll);
            Vector3 targetUpLocal = Orientation.RelativeFrom(targetUp, end);
            float rollComponent = MathF.Atan2(targetUpLocal.Y, targetUpLocal.Z);

            // Get target angular velocity and clip to 5.5 magnitude
            Vector3 targetW = new Vector3(pitchComponent * 7f, yawComponent * 7f, rollComponent * 5f);
            if (targetW.Length() > 5.5f)
            {
                targetW *= 5.5f / targetW.Length();
            }

            float currentYawVel = Vector3.Dot(car.AngularVelocity, car.Orientation.Up);
            float currentPitchVel = - Vector3.Dot(car.AngularVelocity, car.Orientation.Right);
            float currentRollVel = - Vector3.Dot(car.AngularVelocity, car.Orientation.Forward);

            return new Controller()
            {
                Pitch = Math.Clamp(targetW.X - currentPitchVel, -1f, 1f),
                Yaw = Math.Clamp(targetW.Y - currentYawVel, -1f, 1f),
                Roll = Math.Clamp(targetW.Z - currentRollVel, -1f, 1f)
            };
        }

        public static Controller GetController(Car car, Vector3 targetForward)
        {
            targetForward = Vector3.Normalize(targetForward);
            Vector3 targetForwardLocal = Orientation.RelativeFrom(targetForward, car.Orientation);
            float pitchComponent = MathF.Atan2(targetForwardLocal.Z, targetForwardLocal.X);
            float yawComponent = MathF.Atan2(targetForwardLocal.Y, targetForwardLocal.X);
            float rollComponent = 0;

            // Get target angular velocity and clip to 5.5 magnitude
            Vector3 targetW = new Vector3(pitchComponent * 7f, yawComponent * 7f, rollComponent * 3f);
            if (targetW.Length() > 5.5f)
            {
                targetW *= 5.5f / targetW.Length();
            }

            float currentYawVel = Vector3.Dot(car.AngularVelocity, car.Orientation.Up);
            float currentPitchVel = - Vector3.Dot(car.AngularVelocity, car.Orientation.Right);

            return new Controller()
            {
                Pitch = Math.Clamp(targetW.X - currentPitchVel, -1f, 1f),
                Yaw = Math.Clamp(targetW.Y - currentYawVel, -1f, 1f),
                Roll = 0
            };
        }
    }
}
