using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    class Waiter : Agents
    {
        public void WaiterActions(Action<string, object> printWaiterListBox, Puben pub, PubSettings userPubSettings)
        {
            while (userPubSettings.OpenCountdown > 0 || pub.dirtyGlasses.itemCollection.Count > 0 || pub.guestsInPub > 0 || pub.trayOfDirtyGlasses.itemBag.Count > 0)
            {
                if (pub.dirtyGlasses.itemCollection.Count == 0)
                {
                    printWaiterListBox("Waiting for glasses to clean", this);
                    while (pub.dirtyGlasses.itemCollection.Count == 0)
                    {
                        Thread.Sleep(100);
                    }
                }
                if (pub.dirtyGlasses.itemCollection.Count > 0)
                {
                    printWaiterListBox("Picking up dirty glasses", this);
                    TimeToWait(userPubSettings.WaiterPickUpGlassesTime, userPubSettings.PubSimulationSpeed);
                    foreach (var dirtyGlass in pub.dirtyGlasses.itemCollection)
                    {
                        pub.trayOfDirtyGlasses.itemBag.Add(new GlassOnTray());
                        pub.dirtyGlasses.itemCollection.Take();
                    }
                    printWaiterListBox("Cleaning glasses", this);
                    TimeToWait(userPubSettings.WaiterCleanGlassesTime, userPubSettings.PubSimulationSpeed);
                    foreach (var glassOnTrayOut in pub.trayOfDirtyGlasses.itemBag)
                    {
                        pub.cleanGlasses.itemBag.Add(new CleanGlass());
                        pub.trayOfDirtyGlasses.itemBag.TryTake(out pub.glassOnTray);
                    }
                    printWaiterListBox("Putting glasses on shelf", this);
                }                
            }
            printWaiterListBox("Waiter goes home", this);
            userPubSettings.PubOpenButton = true;
        }
    }
}
