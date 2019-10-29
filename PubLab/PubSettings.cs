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
        public static PubSettings MyInstance()
        {
            return instance;
        }

        private int numOfGlasses = 8;
        private int numOfChairs = 9;
        private int openDuration = 120;
        private int bouncerMaxNewGuestTimer = 10;
        private int bouncerMinNewGuestTimer = 3;

    }
}
