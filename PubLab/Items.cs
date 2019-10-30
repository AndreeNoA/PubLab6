using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    public class ItemsBag<T> where T : class, new()
    {
        public T item;

        public ConcurrentBag<T> itemBag = new ConcurrentBag<T>();

        public void CreateItems(T item, int count) //bytt namn
        {
            for (int i = 0; i < count; i++)
            {
                itemBag.Add(new T());
            }
        }

        public ItemsBag() { item = new T(); }
    }

    public class ItemsCollection<T> where T : class, new()
    {
        public T item;

        public BlockingCollection<T> itemCollection = new BlockingCollection<T>();

        public ItemsCollection() { item = new T(); }
    }


    public class Chair { }

    public class CleanGlass { }

    public class DirtyGlass { }
    public class GlassOnTray { }
}