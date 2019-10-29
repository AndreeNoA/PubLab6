using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab.Agents
{
    public abstract class AgentsBase
    {
        public static void TimeToWait(int milliseconds)
        {
            Thread.Sleep(Convert.ToInt32(milliseconds * 1000));
        }
    }
}
