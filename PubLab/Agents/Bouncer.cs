using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    public class Bouncer : Agents
    {
        public void BouncerActions(Action<string, object> logText, Puben pub, PubSettings userPubSettings)
        {
            while (true)
            {
                while (userPubSettings.IsBusWithGuestsComing is true)
                {
                    if (userPubSettings.OpenCountdown < 100)
                    {
                        userPubSettings.NumberOfEnteringGuests = 15;                        
                        CreateGuest(logText, pub, userPubSettings);
                        userPubSettings.IsBusWithGuestsComing = false;
                        userPubSettings.NumberOfEnteringGuests = 1;
                    }
                    else if (userPubSettings.OpenCountdown > userPubSettings.BouncerMaxNewGuestTimer)
                    {
                        TimeToWait(pub.random.Next(userPubSettings.BouncerMinNewGuestTimer, userPubSettings.BouncerMaxNewGuestTimer), userPubSettings.PubSimulationSpeed);
                        CreateGuest(logText, pub, userPubSettings);
                    }

                }
                if (userPubSettings.OpenCountdown > userPubSettings.BouncerMaxNewGuestTimer)
                {
                    TimeToWait(pub.random.Next(userPubSettings.BouncerMinNewGuestTimer, userPubSettings.BouncerMaxNewGuestTimer), userPubSettings.PubSimulationSpeed);
                    CreateGuest(logText, pub, userPubSettings);
                }
                else if (userPubSettings.OpenCountdown == 0)
                {
                    logText("Bouncern goes home", this);
                    break;
                }
            }
        }

        public void CreateGuest(Action<string, object> logText, Puben pub, PubSettings pubset)
        {
            for (int i = 0; i < pubset.NumberOfEnteringGuests; i++)
            {
                Task.Run(() =>
                  {                    
                    Guest guest = new Guest(pub);
                    guest.GuestActions(logText, pub, pubset);
                    Thread.Sleep(10);
                  });
            }
        }
    }
}
