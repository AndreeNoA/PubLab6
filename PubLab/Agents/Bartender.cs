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
        public void BartenderActions(Action<string, object> printBartenderListBox, Items<Glass> glasses)
        {
            while (PubSettings.MyInstance().openCountdown > 0)
            {
                if (Guest.guestsInPub == 0)
                {
                    printBartenderListBox("Bartender waiting for guest", this);
                    TimeToWait(3);
                }
                if (glasses.GetNumOfItems() > 0)
                {
                    printBartenderListBox("Takes a glass", this);
                    TimeToWait(PubSettings.MyInstance().bartenderFetchGlassTime);
                    glasses.itemQueue.TryTake(out glass);
                    printBartenderListBox("Pours beer to ?", this);
                    TimeToWait(PubSettings.MyInstance().bartenderPourBeerTime);
                }
            }
        }
    }
}
