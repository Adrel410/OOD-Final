using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld {
    public interface IWorldEvent {

        Room Trigger {
            get;
        }

        void Execute();


    }

    public interface IRoomDelegate {

        Room ContainingRoom {
            set; get;
        }

        Door GetExit(string exitName);
        string GetExits();
        string Description();
        NPC GetNPC(string npcName);
        string GetNPCS();
    }

    public interface ICloseable {
        bool IsClosed {
            get;
        }

        bool IsOpen {
            get;
        }

        bool Open();
        bool Close();
    }

    
     public interface ILockable {
        bool IsLocked {
            get;
        }

        bool IsUnlocked {
            get;
        }

        bool Unlock();
        bool Lock();

        bool CanOpen {
            get;
        }

        bool CanClose {
            get;
        }

        

     }
    public interface INPC
    {
        string Name
        {
            get;
        }

        float Power
        {
            get;
        }
        float Value
        {
            get;
        }

        void Add(INPC nPC);
    }

    public interface IItem {
        string Name {
            get;
        }

        float Weight {
            get;
        }

        string Description {
            get;
        }
        float Value
        {
            get;
        }

        string LongName { get; }
        bool IsDecorator { get; }

        void AddDecorator(IItem decorator);
    }
    //has to implement everything contained in the Iitem //sub interface
    public interface IItemContainer : IItem
    {
 
        void Insert(IItem item);
        IItem Remove(string itemName);

    }
    //public interface IINPC : INPC
    //{

    //    void Add(INPC NPC);
    //    INPC Remove(string npcName);

    //}


}
