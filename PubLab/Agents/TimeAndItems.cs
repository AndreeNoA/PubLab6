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
        public static int guestsInPub;
        public static Random random = new Random();
        public static CleanGlass cleanGlass = new CleanGlass();
        public static DirtyGlass dirtyGlass = new DirtyGlass();
        public static GlassOnTray glassOnTray = new GlassOnTray();
        public static Chair chair = new Chair();
        public static BlockingCollection<Guest> QueueAtBar = new BlockingCollection<Guest>();
        public static BlockingCollection<Guest> QueueToChair = new BlockingCollection<Guest>();
        public Action<string, object> LogText { get; set; }
        public static void TimeToWait(int milliseconds)
        {
            Thread.Sleep(Convert.ToInt32(milliseconds * 1000));
        }
    }

}
