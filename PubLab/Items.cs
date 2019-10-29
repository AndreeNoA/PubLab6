using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    public class Items<T> where T : class, new()
    {
        public T item;

        public ConcurrentBag<T> itemQueue = new ConcurrentBag<T>();

        public void CreateItems(T item, int count) //bytt namn
        {
            for (int i = 0; i < count; i++)
            {
                itemQueue.Add(new T());
            }
        }

        public int GetNumOfItems()
        {
            int n = 0;
            for (int i = 1; i <= itemQueue.Count; i++)
            {
                n++;
            }
            return n;
        }

        public Items() { item = new T(); }
    }
    public class Chair { }

    public class Glass { }

    public class UsedGlass { }
}