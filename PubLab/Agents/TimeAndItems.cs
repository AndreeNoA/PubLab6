using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    
    public abstract class TimeAndItems
    {
        public static Glass glass = new Glass();
        public static void TimeToWait(int milliseconds)
        {
            Thread.Sleep(Convert.ToInt32(milliseconds * 1000));
        }
    }

}
