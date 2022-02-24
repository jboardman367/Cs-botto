using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bot.Game
{
    public class Car
    {
        public Car(rlbot.flat.PlayerInfo playerInfo, bool inverted)
        {
            this.inverted = inverted;
            var physics = playerInfo.Physics.Value;
            if (inverted)
            {
                Location = new Vector3(-physics.Location.Value.X, -physics.Location.Value.Y, physics.Location.Value.Z);
                Velocity = new Vector3(-physics.Velocity.Value.X, -physics.Velocity.Value.Y, physics.Velocity.Value.Z);
                AngularVelocity = new Vector3(-physics.AngularVelocity.Value.X, -physics.AngularVelocity.Value.Y, -physics.AngularVelocity.Value.Z);
            }
            else
            {
                Location = new Vector3(physics.Location.Value.X, physics.Location.Value.Y, physics.Location.Value.Z);
                Velocity = new Vector3(physics.Velocity.Value.X, physics.Velocity.Value.Y, physics.Velocity.Value.Z);
                AngularVelocity = new Vector3(physics.AngularVelocity.Value.X, physics.AngularVelocity.Value.Y, physics.AngularVelocity.Value.Z);
            }
            Orientation = new Orientation(physics.Rotation, inverted);
            Boost = playerInfo.Boost;
            IsGrounded = playerInfo.HasWheelContact;
            FlipTimer = -1;
            Name = playerInfo.Name;
            lastVelocity = Velocity;
            Team = playerInfo.Team;
        }

        public void Update(rlbot.flat.PlayerInfo playerInfo, float dt)
        {
            var physics = playerInfo.Physics.Value;
            if (inverted)
            {
                Location = new Vector3(-physics.Location.Value.X, -physics.Location.Value.Y, physics.Location.Value.Z);
                Velocity = new Vector3(-physics.Velocity.Value.X, -physics.Velocity.Value.Y, physics.Velocity.Value.Z);
                AngularVelocity = new Vector3(-physics.AngularVelocity.Value.X, -physics.AngularVelocity.Value.Y, -physics.AngularVelocity.Value.Z);
            }
            else
            {
                Location = new Vector3(physics.Location.Value.X, physics.Location.Value.Y, physics.Location.Value.Z);
                Velocity = new Vector3(physics.Velocity.Value.X, physics.Velocity.Value.Y, physics.Velocity.Value.Z);
                AngularVelocity = new Vector3(physics.AngularVelocity.Value.X, physics.AngularVelocity.Value.Y, physics.AngularVelocity.Value.Z);
            }
            Orientation = new Orientation(physics.Rotation, inverted);
            Boost = playerInfo.Boost;
            IsGrounded = playerInfo.HasWheelContact;
            Team = playerInfo.Team;

            Vector3 accel = (Velocity - lastVelocity) / dt;

            // Update the flip info
            if (playerInfo.DoubleJumped)
            {
                FlipTimer = 0;
            }
            else if (IsGrounded) 
            {
                FlipTimer = -1;
                jumpReset = true;
            }
            else
            {
                bool holdingJump = Vector3.Dot(accel + 650 * Vector3.UnitZ, Orientation.Up) > 500;
                if (FlipTimer == -1 && jumpReset && playerInfo.Jumped)
                {
                    jumpReset = false;
                }
                else if (FlipTimer == -1 && !jumpReset && !holdingJump)
                {
                    FlipTimer = 1.25f;
                }
                else if (FlipTimer > 0)
                {
                    FlipTimer -= MathF.Min(dt, FlipTimer);
                }
            }
            Name = playerInfo.Name;

            // Store last state values
            lastVelocity = Velocity;
        }

        readonly bool inverted;
        private Vector3 lastVelocity;
        private bool jumpReset;
        public Vector3 Location { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 AngularVelocity { get; private set; }
        public Orientation Orientation { get; private set; }
        public int Boost { get; private set; }
        public bool IsGrounded { get; private set; }
        public float FlipTimer { get; private set; }
        public string Name { get; private set; }
        public int Team { get; private set; }

    }
}
