using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    public class Bartender : TimeAndLog
    {
        public void BartenderActions(Action<string, object> printBartenderListBox, Puben pub, PubSettings pubset)
        {
            while (pubset.OpenCountdown > 0 || pub.guestsInPub > 0)
            {
                if (pub.guestsInPub == 0 && pubset.OpenCountdown > 0)
                {
                    printBartenderListBox("Bartender waiting for guest", this);
                    TimeToWait(5);
                }
                if (pub.QueueAtBar.Count > 0)
                {
                    printBartenderListBox("Goes to shelf", this);
                    while (pub.cleanGlasses.itemBag.Count == 0)
                        Thread.Sleep(1000);

                    if (pub.cleanGlasses.itemBag.Count > 0)
                    {
                        TimeToWait(pubset.BartenderFetchGlassTime);
                        pub.cleanGlasses.itemBag.TryTake(out pub.cleanGlass);
                        printBartenderListBox("Pours beer to " + pub.QueueAtBar.First().Name, this);                    
                        TimeToWait(pubset.BartenderPourBeerTime);
                        pub.QueueToChair.Add(pub.QueueAtBar.First());
                        pub.QueueAtBar.Take();
                    }
                }
                if (pub.QueueAtBar.Count == 0 && pubset.OpenCountdown == 0)
                {
                    printBartenderListBox("Bartender goes home", this);
                    break;
                }
            }
        }
    }
}
