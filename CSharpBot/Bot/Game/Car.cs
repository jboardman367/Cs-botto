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
        }

        public void Update(rlbot.flat.PlayerInfo playerInfo, float dt)
        {

        }

        readonly bool inverted;
        public Vector3 Location { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 AngularVelocity { get; private set; }
        public Orientation Orientation { get; private set; }
        public int Boost { get; private set; }
        public bool IsGrounded { get; private set; }
        public float FlipTimer { get; private set; }  // TODO: add flip reset logic (check dv dot up > val => jumped)
        public string Name { get; private set; }

    }
}
