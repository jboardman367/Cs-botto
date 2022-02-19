using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bot.Game
{
    public static class GameState
    {
        static GameState()
        {

        }

        public static void Update(rlbot.flat.GameTickPacket packet, rlbot.flat.FieldInfo fieldInfo, rlbot.flat.BallPrediction ballPrediction, int index)
        {
            // We will pretend we are always Blue
            bool inverted = packet.Players(index).Value.Team == 1;

            // Update the ball
            Ball.Update(packet, ballPrediction, inverted);

            // Update the boosts
            Boosts ??= CreateBoosts(fieldInfo, inverted);
            for (int i = 0; i < fieldInfo.BoostPadsLength; i++)
            {
                if (!fieldInfo.BoostPads(i).Value.IsFullBoost)
                {
                    Boosts[i].StepCooldown(packet.GameInfo.Value.SecondsElapsed - lastGameSeconds);
                }
            }

            // Update the cars
            Cars ??= CreateCars(packet, inverted);

            // Update the last seconds for dt
            lastGameSeconds = packet.GameInfo.Value.SecondsElapsed;
        }

        static Boost[] CreateBoosts(rlbot.flat.FieldInfo fieldInfo, bool inverted)
        {
            Boost[] boosts = new Boost[fieldInfo.BoostPadsLength];
            for (int i = 0; i < fieldInfo.BoostPadsLength; i++)
            {
                boosts[i] = new Boost(i, fieldInfo, inverted);
            }
            return boosts;
        }

        static Car[] CreateCars(rlbot.flat.GameTickPacket packet, bool inverted)
        {
            Car[] cars = new Car[packet.PlayersLength];
            for (int i = 0; i < packet.PlayersLength; i++)
            {
                var player = packet.Players(i).Value;
                cars[i] = new Car(player, inverted);
            }
            return cars;
        }

        public static Car[] Cars { get; private set; }

        public static Ball.Ball Ball { get; private set; }

        public static Boost[] Boosts { get; private set; } = null;

        static float lastGameSeconds = 0;
    }
}
