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
            Children.Add("kickoff", new States.Kickoff.Kickoff());
        }

        public static void ChangeState(string state)
        {

        }

        public static Dictionary<string, State> Children = new Dictionary<string, State>();
    }
}
