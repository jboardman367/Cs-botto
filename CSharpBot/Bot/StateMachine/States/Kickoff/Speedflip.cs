using Bot.Game;
using RLBotDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine.States.Kickoff
{
    public class Speedflip : State
    {
        public override void Enter()
        {
            Console.WriteLine("Entering state kickoff.speedflip");
            if (MathF.Abs(GameState.Me.Location.X + 2048) < 10)
                type = KickoffType.ShortRight;
            else if (MathF.Abs(GameState.Me.Location.X - 2048) < 10)
                type = KickoffType.ShortLeft;
            else if (MathF.Abs(GameState.Me.Location.X + 256) < 10)
                type = KickoffType.LongRight;
            else if (MathF.Abs(GameState.Me.Location.X - 256) < 10)
                type = KickoffType.LongLeft;
            else
                type = KickoffType.Straight;
            steps = 0;
        }

        public override void Exit()
        {
        }

        public override void Step()
        {
            Tuple<int, Controller>[] seq = null;
            switch (type)
            {
                case KickoffType.ShortRight:
                    seq = shortRightSequence;
                    break;
                case KickoffType.ShortLeft:
                    seq = shortLeftSequence;
                    break;
                case KickoffType.LongRight:
                    seq = longRightSequence;
                    break;
                case KickoffType.LongLeft:
                    seq = longLeftSequence;
                    break;
                case KickoffType.Straight:
                    seq = straightSequence;
                    break;
            }

            int step = steps;
            foreach (Tuple<int, Controller> kickoffStep in seq)
            {
                if (step <= kickoffStep.Item1)
                {
                    Controller controller = kickoffStep.Item2;
                    controller.Boost = GameState.Me.Velocity.Length() < 2290;
                    Bot.Controller = controller;
                    steps++;
                    return;
                }
                step -= kickoffStep.Item1;
            }
            EventHandler.OnKickoffSpeedflipComplete();
        }

        KickoffType type;
        int steps;

        static readonly Tuple<int, Controller>[] shortRightSequence = new Tuple<int, Controller>[]
        {
            new(30, new Controller() { Steer=-.6f, Boost=true } ), //0
            new(20, new Controller() { Steer=.4f, Boost=true } ), //1
            new (10, new Controller() { Yaw=1f, Boost=true, Jump=true, Pitch=1f, Handbrake=true } ), //2
            new (2, new Controller() { Yaw=.2f, Boost=true, Pitch=1f } ), //3
            new (2, new Controller() { Yaw=.65f, Pitch=-1f, Boost=true, Jump=true, Roll=.2f } ), //4
            new (70, new Controller() { Pitch=1, Boost=true, Roll=1f } ), //5   
        };

        static readonly Tuple<int, Controller>[] shortLeftSequence = new Tuple<int, Controller>[]
        {
            new(30, new Controller() { Steer=.6f, Boost=true } ), //0
            new(20, new Controller() { Steer=-.4f, Boost=true } ), //1
            new (10, new Controller() { Yaw=-1f, Boost=true, Jump=true, Pitch=1f, Handbrake=true } ), //2
            new (2, new Controller() { Yaw=-.2f, Boost=true, Pitch=1f } ), //3
            new (2, new Controller() { Yaw=-.65f, Pitch=-1f, Boost=true, Jump=true, Roll=-.2f } ), //4
            new (70, new Controller() { Pitch=1, Boost=true, Roll=-1f } ), //5 
        };

        static readonly Tuple<int, Controller>[] longRightSequence = new Tuple<int, Controller>[]
        {
            new(65, new Controller() { Steer=-.4f, Boost=true } ), //0
            new(8, new Controller() { Steer=1, Boost=true } ), //1
            new (10, new Controller() { Yaw=1f, Boost=true, Jump=true, Pitch=1f, Handbrake=true } ), //2
            new (2, new Controller() { Yaw=.2f, Boost=true, Pitch=1f } ), //3
            new (2, new Controller() { Yaw=.65f, Pitch=-1f, Boost=true, Jump=true, Roll=.2f } ), //4
            new (70, new Controller() { Pitch=1, Boost=true, Roll=1f } ), //5         
        };

        static readonly Tuple<int, Controller>[] longLeftSequence = new Tuple<int, Controller>[]
        {
            new(65, new Controller() { Steer=.4f, Boost=true } ), //0
            new(8, new Controller() { Steer=-1, Boost=true } ), //1
            new (10, new Controller() { Yaw=-1f, Boost=true, Jump=true, Pitch=1f, Handbrake=true } ), //2
            new (2, new Controller() { Yaw=-.2f, Boost=true, Pitch=1f } ), //3
            new (2, new Controller() { Yaw=-.65f, Pitch=-1f, Boost=true, Jump=true, Roll=-.2f } ), //4
            new (70, new Controller() { Pitch=1, Boost=true, Roll=-1f } ), //5            
        };

        static readonly Tuple<int, Controller>[] straightSequence = new Tuple<int, Controller>[]
        {
            new(55, new Controller() { Steer=.3f, Boost=true } ), //0
            new(5, new Controller() { Steer=-1, Boost=true } ), //1
            new (10, new Controller() { Yaw=-1f, Boost=true, Jump=true, Pitch=1f, Handbrake=true } ), //2
            new (2, new Controller() { Yaw=-.2f, Boost=true, Pitch=1f } ), //3
            new (2, new Controller() { Yaw=-.65f, Pitch=-1f, Boost=true, Jump=true, Roll=-.2f } ), //4
            new (70, new Controller() { Pitch=1, Boost=true, Roll=-1f } ), //5   
        };
    }

    enum KickoffType
    {
        ShortLeft = 0,
        LongLeft = 1,
        Straight = 2,
        LongRight = 3,
        ShortRight = 4
    }
}
