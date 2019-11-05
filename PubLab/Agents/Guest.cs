using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{

    public class Guest : Agents
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

        public Guest(Puben pub)
        {
            Name = nameList[pub.random.Next(nameList.Length - 1)];
        }

        public void GuestActions(Action<string, object> logText, Puben pub, PubSettings userPubSettings)
        {
            LogText = logText;

            GuestEnter(pub, userPubSettings);
            while (!pub.QueueToChair.Contains(this))
            {
                Thread.Sleep(100);
            }
            GuestLookingForChair(pub, userPubSettings);
            while (pub.chairs.itemBag.Count == 0)
            {
                Thread.Sleep(100);
            }
            GuestDrinks(pub, userPubSettings);
            GuestLeaves(pub, userPubSettings);
        }
        private void GuestEnter(Puben pub, PubSettings userPubSettings)
        {
            pub.guestsInPub++;
            LogText?.Invoke($"{Name} enters BaBar, and goes to the bar", this);
            TimeToWait(userPubSettings.GuestWalkToBar, userPubSettings.PubSimulationSpeed);
            pub.QueueAtBar.Add(this);
        }
        private void GuestLookingForChair(Puben pub, PubSettings userPubSettings)
        {
            LogText?.Invoke($"{Name} takes the beer and looks for a chair", this);
            TimeToWait(userPubSettings.GuestWalkToChair, userPubSettings.PubSimulationSpeed);
        }
        private void GuestDrinks(Puben pub, PubSettings userPubSettings)
        {
            pub.chairs.itemBag.TryTake(out pub.chair);
            LogText?.Invoke($"{Name} sits down and drinks the beer", this);
            TimeToWait(pub.random.Next(userPubSettings.GuestDrinkBeerMinTime, userPubSettings.GuestDrinkBeerMaxTime), userPubSettings.PubSimulationSpeed);
        }
        private void GuestLeaves(Puben pub, PubSettings userPubSettings)
        {
            pub.dirtyGlasses.itemCollection.Add(pub.dirtyGlass);
            LogText?.Invoke($"{Name} leaves the bar", this);
            pub.chairs.itemBag.Add(pub.chair);
            pub.guestsInPub--;
        }
    }
}
