using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bot.Game
{
    public class Boost
    {
        public int Index { get; private set; }
        public float Cooldown { get; private set; }
        public bool IsLarge { get; private set; }
        public Vector3 Location { get; private set; }
        public Boost(int index, rlbot.flat.FieldInfo fieldInfo, bool inverted)
        {
            Index = index;
            Cooldown = 0;
            IsLarge = largeBoosts.Contains(index);
            var loc = fieldInfo.BoostPads(index).Value.Location.Value;
            if (inverted)
            {
                Location = new Vector3(-loc.X, -loc.Y, loc.Z);
            }
            else
            {
                Location = new Vector3(loc.X, loc.Y, loc.Z);
            }
        }

        public void StepCooldown(float dt)
        {
            if (Cooldown == 0)
            {
                Cooldown = IsLarge ? 10 : 4;
                return;
            }
            Cooldown = MathF.Max(Cooldown - dt, 0);
        }

        static readonly int[] largeBoosts = new int[6] { 30, 29, 18, 15, 4, 3 };
    }
}
