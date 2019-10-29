using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    class Waiter : TimeAndItems
    {
        public void WaiterActions(Action<string, object> printBartenderListBox)
        {
            while (PubSettings.MyInstance().openCountdown > 0)
            {
                printBartenderListBox("då", this);
                TimeToWait(2);
            }
        }
    }
}
