using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubLab
{
    public class Bouncer : TimeAndLog
    {
        public void BouncerActions(Action<string, object> logText, Puben pub, PubSettings pubset)
        {
            while (true)
            {
                while (pubset.IsBusWithGuestsComing is true)
                {
                    if (pubset.OpenCountdown < 100)
                    {
                        pubset.NumberOfEnteringGuests = 15;                        
                        CreateGuest(logText, pub, pubset);
                        pubset.IsBusWithGuestsComing = false;
                        pubset.NumberOfEnteringGuests = 1;
                    }
                    else if (pubset.OpenCountdown > pubset.BouncerMaxNewGuestTimer)
                    {
                        TimeToWait(pub.random.Next(pubset.BouncerMinNewGuestTimer, pubset.BouncerMaxNewGuestTimer));
                        CreateGuest(logText, pub, pubset);
                    }

                }
                if (pubset.OpenCountdown > pubset.BouncerMaxNewGuestTimer)
                {
                    TimeToWait(pub.random.Next(pubset.BouncerMinNewGuestTimer, pubset.BouncerMaxNewGuestTimer));
                    CreateGuest(logText, pub, pubset);
                }
                else if (pubset.OpenCountdown == 0)
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
