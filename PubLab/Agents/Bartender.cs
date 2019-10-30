using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    public class Bartender : TimeAndItems
    {
        public void BartenderActions(Action<string, object> printBartenderListBox, ItemsBag<CleanGlass> cleanGlasses)
        {
            while (PubSettings.MyPub().openCountdown > 0 || Guest.guestsInPub > 0)
            {
                if (Guest.guestsInPub == 0 && PubSettings.MyPub().openCountdown > 0)
                {
                    printBartenderListBox("Bartender waiting for guest", this);
                    TimeToWait(5);
                }
                if (QueueAtBar.Count > 0)
                {
                    printBartenderListBox("Goes to shelf", this);
                    while (cleanGlasses.itemBag.Count == 0)
                        Thread.Sleep(1000);

                    if (cleanGlasses.itemBag.Count > 0)
                    {
                        TimeToWait(PubSettings.MyPub().bartenderFetchGlassTime);
                        cleanGlasses.itemBag.TryTake(out cleanGlass);
                        printBartenderListBox("Pours beer to " + QueueAtBar.First().Name, this);                    
                        TimeToWait(PubSettings.MyPub().bartenderPourBeerTime);
                        QueueToChair.Add(QueueAtBar.First());
                        QueueAtBar.Take();
                    }
                }
                if (QueueAtBar.Count == 0 && PubSettings.MyPub().openCountdown == 0)
                {
                    printBartenderListBox("Bartender goes home", this);
                    break;
                }
            }
        }
    }
}
