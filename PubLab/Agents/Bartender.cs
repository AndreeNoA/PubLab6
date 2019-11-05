using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    public class Bartender : Agents
    {
        public void BartenderActions(Action<string, object> printBartenderListBox, Puben pub, PubSettings userPubSettings)
        {
            while (userPubSettings.OpenCountdown > 0 || pub.guestsInPub > 0)
            {
                if (pub.guestsInPub == 0 && userPubSettings.OpenCountdown > 0)
                {
                    printBartenderListBox("Bartender waiting for guest", this);
                    while (pub.guestsInPub == 0 && userPubSettings.OpenCountdown > 0)
                    {
                        Thread.Sleep(100);
                    }
                }
                if (pub.QueueAtBar.Count > 0)
                {
                    printBartenderListBox("Bartender goes to shelf", this); 
                    while (pub.cleanGlasses.itemBag.Count == 0)
                        Thread.Sleep(100);

                    if (pub.cleanGlasses.itemBag.Count > 0)
                    {
                        TimeToWait(userPubSettings.BartenderFetchGlassTime, userPubSettings.PubSimulationSpeed);
                        pub.cleanGlasses.itemBag.TryTake(out pub.cleanGlass);
                        printBartenderListBox("Pours beer to " + pub.QueueAtBar.First().Name, this);                    
                        TimeToWait(userPubSettings.BartenderPourBeerTime, userPubSettings.PubSimulationSpeed);
                        pub.QueueToChair.Add(pub.QueueAtBar.First());
                        pub.QueueAtBar.Take();
                    }
                }
                if (pub.QueueAtBar.Count == 0 && userPubSettings.OpenCountdown == 0)
                {
                    printBartenderListBox("Bartender goes home", this);
                    break;
                }
            }
        }
    }
}
