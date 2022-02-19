using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.StateMachine
{
    public static class StateMachine
    {
        static StateMachine()
        {
            children.Add("kickoff", new States.Kickoff.Kickoff());
        }

        public static void ChangeState(string stateStr)
        {
            string[] states = stateStr.Split('.');
            State nextChild = children.GetValueOrDefault(states[0], null);
            if (nextChild == null)
            {
                throw new KeyNotFoundException("No state " + states[0] + " found.");
            }
            if (child != nextChild)
            {
                child = nextChild;
                child.Enter();
            }

            State lastChild = child;
            for (int i = 1; i < states.Length; i++)
            {
                nextChild = lastChild.Children.GetValueOrDefault(states[i], null);
                if (nextChild == null)
                {
                    throw new KeyNotFoundException("No state " + String.Join(".", states[..i]) + " found.");
                }
                if (lastChild.Child != nextChild)
                {
                    lastChild = nextChild;
                    lastChild.Enter();
                }
                lastChild = lastChild.Child;
            }
            Console.WriteLine("Entered state " + stateStr);
        }

        public static void Step()
        {

        }

        static readonly Dictionary<string, State> children = new Dictionary<string, State>();
        public static Dictionary<string, State> Children { get { return children; } }
        static State child;
        public static State Child { get { return child; } }
    }
}
