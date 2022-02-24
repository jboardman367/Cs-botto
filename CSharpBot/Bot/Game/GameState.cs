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
            Ball = new Ball.Ball();
        }

        public static void Update(rlbot.flat.GameTickPacket packet, rlbot.flat.FieldInfo fieldInfo, rlbot.flat.BallPrediction ballPrediction, int index)
        {
            // We will pretend we are always Blue
            bool inverted = packet.Players(index).Value.Team == 1;

            // Calculate dt
            float dt = packet.GameInfo.Value.SecondsElapsed - lastGameSeconds;

            // Update the cars
            Cars ??= CreateCars(packet, inverted, index);

            for (int i = 0; i < packet.PlayersLength; i++)
            {
                Cars[i].Update(packet.Players(i).Value, dt);
            }

            // Update the ball
            Ball.Update(packet, ballPrediction, inverted);

            // Update the boosts
            Boosts ??= CreateBoosts(fieldInfo, inverted);
            for (int i = 0; i < fieldInfo.BoostPadsLength; i++)
            {
                if (!fieldInfo.BoostPads(i).Value.IsFullBoost)
                {
                    Boosts[i].StepCooldown(dt);
                }
            }

            // Update the misc values
            GoalDif = inverted ? packet.Teams(1).Value.Score - packet.Teams(0).Value.Score : packet.Teams(0).Value.Score - packet.Teams(1).Value.Score;
            SecondsRemaining = packet.GameInfo.Value.GameTimeRemaining;
            IsKickoffPause = packet.GameInfo.Value.IsKickoffPause;

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

        static Car[] CreateCars(rlbot.flat.GameTickPacket packet, bool inverted, int index)
        {
            Car[] cars = new Car[packet.PlayersLength];
            for (int i = 0; i < packet.PlayersLength; i++)
            {
                var player = packet.Players(i).Value;
                cars[i] = new Car(player, inverted);
            }
            Me = cars[index];
            Allies = cars.Where(x => x.Team == Me.Team && x != Me).ToArray();
            Console.WriteLine(Allies.Length.ToString());
            Opponents = cars.Where(x => x.Team != Me.Team).ToArray();
            Console.WriteLine(Opponents.Length.ToString());

            return cars;
        }

        public static Car[] Cars { get; private set; }

        public static Car[] Allies { get; private set; }

        public static Car[] Opponents { get; private set; }

        public static Car Me { get; private set; }

        public static Ball.Ball Ball { get; private set; }

        public static Boost[] Boosts { get; private set; } = null;

        public static int GoalDif { get; private set; }

        public static float SecondsRemaining { get; private set; }

        static float lastGameSeconds = 0;

        public static bool IsKickoffPause { get; private set; }
    }
}
