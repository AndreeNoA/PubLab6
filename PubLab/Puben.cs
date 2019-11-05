using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLab
{
    public class Puben
    {
        public Chair chair = new Chair();
        public Random random = new Random();
        public CleanGlass cleanGlass = new CleanGlass();
        public DirtyGlass dirtyGlass = new DirtyGlass();
        public GlassOnTray glassOnTray = new GlassOnTray();
        public BlockingCollection<Guest> QueueAtBar = new BlockingCollection<Guest>();
        public BlockingCollection<Guest> QueueToChair = new BlockingCollection<Guest>();
        
        public ItemsBag<Chair> chairs;
        public ItemsBag<CleanGlass> cleanGlasses;
        public ItemsCollection<DirtyGlass> dirtyGlasses;
        public ItemsBag<GlassOnTray> trayOfDirtyGlasses;
        
        public int guestsInPub;

        public void CreatePub(PubSettings userPubSettings)
        {
            chairs = new ItemsBag<Chair>();
            cleanGlasses = new ItemsBag<CleanGlass>();
            dirtyGlasses = new ItemsCollection<DirtyGlass>();
            trayOfDirtyGlasses = new ItemsBag<GlassOnTray>();
            chairs.CreateBagOfItems(new Chair(), userPubSettings.NumOfChairs);
            cleanGlasses.CreateBagOfItems(new CleanGlass(), userPubSettings.NumOfGlasses);
        }
    }
    public class ItemsBag<T> where T : class, new()
    {
        public T item;

        public ConcurrentBag<T> itemBag = new ConcurrentBag<T>();

        public void CreateBagOfItems(T item, int count)
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