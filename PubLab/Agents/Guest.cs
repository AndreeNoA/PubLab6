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
            bool isChairFree = MainWindow.chairs.itemBag.TryPeek(out chair);
            LogText = logText;

            GuestEnter();
            while (!QueueToChair.Contains(this))
            {
                Thread.Sleep(100);
            }
            GuestLookingForChair();
            while (MainWindow.chairs.itemBag.Count == 0)
            {
                Thread.Sleep(100);
            }
            GuestDrinks();
            GuestLeaves();
        }
        private void GuestEnter()
        {
            guestsInPub++;
            LogText?.Invoke($"{Name} enters BaBar, and goes to the bar", this);
            TimeToWait(PubSettings.MyPub().guestWalkToBar);
            QueueAtBar.Add(this);           
        }
        private void GuestLookingForChair()
        {
            LogText?.Invoke($"{Name} takes the beer and looks for a chair", this);
            TimeToWait(PubSettings.MyPub().guestWalkToChair);
        }
        private void GuestDrinks()
        {
            MainWindow.chairs.itemBag.TryTake(out chair);
            LogText?.Invoke($"{Name} sits down and drinks the beer", this);
            TimeToWait(random.Next(PubSettings.MyPub().guestDrinkBeerMinTime, PubSettings.MyPub().guestDrinkBeerMaxTime));
        }
        private void GuestLeaves()
        {
            MainWindow.dirtyGlasses.itemCollection.Add(dirtyGlass);
            LogText?.Invoke($"{Name} leaves the bar", this);
            MainWindow.chairs.itemBag.Add(chair);
            guestsInPub--;
        }
    }
}
