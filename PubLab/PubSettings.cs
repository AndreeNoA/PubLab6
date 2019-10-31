using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    public class PubSettings
    {
        private PubSettings()
        {
        }
        private static PubSettings instance = new PubSettings();
        public static PubSettings MyPub()
        {
            return instance;
        }

        public int numOfUsedGlasses;
        public int numOfGlasses = 8; // 8
        public int numOfChairs = 9; // 9
        public int openDuration = 120; // 120 
        public int openCountdown;
        public int bouncerMinNewGuestTimer = 3; // 3
        public int bouncerMaxNewGuestTimer = 10; // 10
        public int waiterPickUpGlassesTime = 10; // 10
        public int waiterCleanGlassesTime = 15; // 15
        public int bartenderFetchGlassTime = 3; // 3
        public int bartenderPourBeerTime = 3; // 3
        public int guestWalkToBar = 1; // 1
        public int guestWalkToChair = 4; // 4
        public int guestDrinkBeerMaxTime = 30; //30
        public int guestDrinkBeerMinTime = 20; //20
        public bool pubOpenButton = false;

    }
}
