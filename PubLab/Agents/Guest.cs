using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab.Agents
{

    public class Guest : AgentsBase
    {
        public string Name { get; private set; }
        Random random = new Random();
        public static int guestsInPub;
        private Action<string, object> LogText { get; set; }
        public List<Guest> CurrentGuests { get; private set; }


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
            LogText = logText;
            GuestEnter();            
            GuestDrinks();
            GuestLeaves();
        }
        private void GuestEnter()
        {
            guestsInPub++;
            LogText?.Invoke($"{Name} enters the bar", this);
        }
        private void GuestDrinks()
        {
            TimeToWait(random.Next(5, 10));
            LogText?.Invoke($"{Name} drinks a beer", this);
        }
        private void GuestLeaves()
        {
            TimeToWait(random.Next(5, 8));
            LogText?.Invoke($"{Name} leaves the bar", this);
            guestsInPub--;
        }
    }
}
