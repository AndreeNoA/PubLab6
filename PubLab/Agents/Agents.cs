using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{    
    public abstract class Agents
    {
        public Action<string, object> LogText { get; set; }

        public static void TimeToWait(int milliseconds, int pubSimulationSpeed)
        {
            Thread.Sleep(Convert.ToInt32((milliseconds * 1000) / pubSimulationSpeed));
        }

    }

}
