using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{

    public class Guest : TimeAndItems
    {
        public string Name { get; private set; }
        Random random = new Random();
        public static int guestsInPub;
        Chair chair = new Chair();
        //Glass glass = new Glass();
        private Action<string, object> LogText { get; set; }

        public static string[] nameList = new string[]
        {
            "Andreé",
            "Johan",
            "Erik",
            "Anders",
            "Erika",
            "Emma",
            "Hilda",
            "Jonathan",
            "Fredrik",
            "Sofia",
            "Johanna",
            "Pontus",
            "Mattias",
            "Olle",
            "Tina",
            "Jasmine",
            "Monica",
            "Mathilda",
            "Jonas",
            "Emil",
            "Sara",
            "Micke",
            "Kungen",
            "Peter",
            "Lena",
            "Jacob",
            "Filip",
            "Åsa"
        };

        public Guest()
        {
            Name = nameList[random.Next(nameList.Length - 1)];
        }

        public void GuestActions(Action<string, object> logText)
        {
            bool isChairFree = MainWindow.chairs.itemQueue.TryPeek(out chair);
            LogText = logText;

            GuestEnter();
            GuestLookingForChair();
            while (!isChairFree)
            {
                Thread.Sleep(100);
            }
            GuestDrinks();
            //GuestDrinks();
            GuestLeaves();
        }
        private void GuestEnter()
        {
            guestsInPub++;
            LogText?.Invoke($"{Name} enters BaBar, and goes to the bar", this);
            TimeToWait(PubSettings.MyInstance().guestWalkToBar);
            // add guest to guestQueue
            // give beer to guest when first in queue
            
        }
        private void GuestLookingForChair()
        {
            LogText?.Invoke($"{Name} takes the beer and looks for a chair", this);
            TimeToWait(PubSettings.MyInstance().guestWalkToChair);
        }
        private void GuestDrinks()
        {
            MainWindow.chairs.itemQueue.TryTake(out chair);
            LogText?.Invoke($"{Name} sits down and drinks the beer", this);
            TimeToWait(random.Next(PubSettings.MyInstance().guestDrinkBeerMinTime, PubSettings.MyInstance().guestDrinkBeerMaxTime));
        }
        private void GuestLeaves()
        {
            LogText?.Invoke($"{Name} leaves the bar", this);
            MainWindow.chairs.itemQueue.Add(chair);
            guestsInPub--;
        }
    }
}
