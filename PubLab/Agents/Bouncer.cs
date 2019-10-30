using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    public class Bouncer : TimeAndItems
    {
        public void BouncerActions(Action<string, object> logText)
        {
            while (true)
            {
                if (PubSettings.MyPub().openCountdown > PubSettings.MyPub().bouncerMaxNewGuestTimer)
                {
                    TimeToWait(random.Next(PubSettings.MyPub().bouncerMinNewGuestTimer, PubSettings.MyPub().bouncerMaxNewGuestTimer));
                    CreateGuest(logText);
                }
                else if (PubSettings.MyPub().openCountdown == 0)
                {
                    logText("Bouncern goes home", this);
                    break;
                }
            }
        }
        public void CreateGuest(Action<string, object> logText)
        {
            Task.Run(() =>
            {
                Guest guest = new Guest();
                guest.GuestActions(logText);
            });
        }
    }
}
