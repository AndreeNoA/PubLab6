using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    class Waiter : TimeAndItems
    {
        public void WaiterActions(Action<string, object> printWaiterListBox, ItemsBag<CleanGlass> cleanGlasses, ItemsCollection<DirtyGlass> dirtyGlasses, ItemsBag<GlassOnTray> glassesOnTray)
        {
            while (PubSettings.MyPub().openCountdown > 0 || dirtyGlasses.itemCollection.Count > 0 || guestsInPub > 0 || glassesOnTray.itemBag.Count > 0)
            {
                if (dirtyGlasses.itemCollection.Count == 0)
                {
                    printWaiterListBox("Waiting for glasses to clean", this);
                    TimeToWait(10);
                }
                if (dirtyGlasses.itemCollection.Count > 0)
                {
                    printWaiterListBox("Picking up dirty glasses", this);
                    TimeToWait(PubSettings.MyPub().waiterPickUpGlassesTime);
                    foreach (var dirtyGlass in dirtyGlasses.itemCollection)
                    {
                        glassesOnTray.itemBag.Add(new GlassOnTray());
                        dirtyGlasses.itemCollection.Take();
                    }
                    printWaiterListBox("Cleaning glasses", this);
                    TimeToWait(PubSettings.MyPub().waiterCleanGlassesTime);
                    foreach (var glassOnTrayOut in glassesOnTray.itemBag)
                    {
                        cleanGlasses.itemBag.Add(new CleanGlass());
                        glassesOnTray.itemBag.TryTake(out glassOnTray);
                    }
                    printWaiterListBox("Putting glasses on shelf", this);
                }                
            }
            printWaiterListBox("Waiter goes home", this);
            PubSettings.MyPub().pubOpenButton = true;
        }
    }
}
