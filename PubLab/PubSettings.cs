using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    public class PubSettings
    {                 
        private int numOfGlasses = 8; // 8
        private int numOfChairs = 9; // 9
        private int openDuration = 120; // 120 
        private int openCountdown;
        private int bouncerMinNewGuestTimer = 3; // 3
        private int bouncerMaxNewGuestTimer = 10; // 10
        private int waiterPickUpGlassesTime = 10; // 10
        private int waiterCleanGlassesTime = 15; // 15
        private int bartenderFetchGlassTime = 3; // 3
        private int bartenderPourBeerTime = 3; // 3
        private int guestWalkToBar = 1; // 1
        private int guestWalkToChair = 4; // 4
        private int guestDrinkBeerMaxTime = 30; //30
        private int guestDrinkBeerMinTime = 20; //20
        private bool pubOpenButton = false;
        private int numberOfEnteringGuests = 1;
        private bool isBusWithGuestsComing = false;
        private int pubSimulationSpeed = 1;

        public int NumOfGlasses { get => numOfGlasses; set => numOfGlasses = value; }
        public int NumOfChairs { get => numOfChairs; set => numOfChairs = value; }
        public int BarOpenDuration { get => openDuration; set => openDuration = value; }
        public int OpenCountdown { get => openCountdown; set => openCountdown = value; }
        public int BouncerMinNewGuestTimer { get => bouncerMinNewGuestTimer; set => bouncerMinNewGuestTimer = value; }
        public int BouncerMaxNewGuestTimer { get => bouncerMaxNewGuestTimer; set => bouncerMaxNewGuestTimer = value; }
        public int WaiterPickUpGlassesTime { get => waiterPickUpGlassesTime; set => waiterPickUpGlassesTime = value; }
        public int WaiterCleanGlassesTime { get => waiterCleanGlassesTime; set => waiterCleanGlassesTime = value; }
        public int BartenderFetchGlassTime { get => bartenderFetchGlassTime; set => bartenderFetchGlassTime = value; }
        public int BartenderPourBeerTime { get => bartenderPourBeerTime; set => bartenderPourBeerTime = value; }
        public int GuestWalkToBar { get => guestWalkToBar; set => guestWalkToBar = value; }
        public int GuestWalkToChair { get => guestWalkToChair; set => guestWalkToChair = value; }
        public int GuestDrinkBeerMaxTime { get => guestDrinkBeerMaxTime; set => guestDrinkBeerMaxTime = value; }
        public int GuestDrinkBeerMinTime { get => guestDrinkBeerMinTime; set => guestDrinkBeerMinTime = value; }
        public bool PubOpenButton { get => pubOpenButton; set => pubOpenButton = value; }
        public int NumberOfEnteringGuests { get => numberOfEnteringGuests; set => numberOfEnteringGuests = value; }
        public bool IsBusWithGuestsComing { get => isBusWithGuestsComing; set => isBusWithGuestsComing = value; }
        public int PubSimulationSpeed { get => pubSimulationSpeed; set => pubSimulationSpeed = value; }
    }

    public class UserPubSettings : PubSettings
    {
        public void TwentyGlassesThreeChairs()
        {            
            NumOfGlasses = 20;
            NumOfChairs = 3;
        }
        public void FiveGlassesTwentyChairs()
        {
            NumOfGlasses = 5;
            NumOfChairs = 20;
        } 
        public void SlowGuests()
        {
            GuestDrinkBeerMaxTime = 60;
            GuestDrinkBeerMinTime = 40;
        }
        public void WaiterOnSpeed()
        {
            WaiterPickUpGlassesTime = 5;
            WaiterCleanGlassesTime = 7;
        }
        public void BarDoubleOpenTime()
        {
            BarOpenDuration = 300;
        }
        public void CouplesNight()
        {
            NumberOfEnteringGuests = 2;
        }
        public void BusOfGuests()
        {
            BouncerMinNewGuestTimer = 6;
            BouncerMaxNewGuestTimer = 20;
            IsBusWithGuestsComing = true;
        }
    }
}
        

