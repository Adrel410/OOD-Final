using System.Collections;
using System.Collections.Generic;
using System;

namespace HellWorld
{

    public class TrapRoom : IRoomDelegate {

        public Room ContainingRoom {
            set; get;
        }

        public string UnlockWord {
            get; set;
        }

        public TrapRoom() : this("password") {

        }


        //Designated constructor
        public TrapRoom(string unlockWord) {
            UnlockWord = unlockWord;
            NotificationCenter.Instance.AddObserver("PlayerDidSayWord", PlayerDidSayWord);
        }

        public Door GetExit(string exitName) {
            return null;
        }
        public string GetExits() {
            return "You cannot escape.";
        }
        public string Description() {
            return "You have been trapped.";
        }

        public void PlayerDidSayWord(Notification notification) {
            Player player = (Player)notification.Object;
            if (player!= null) {
                if (player.CurrentRoom.RoomDelegate == this) {
                Dictionary<string, Object> userInfo = notification.UserInfo;
                string word = (string)userInfo["word"];
                    //player.OutputMessage("Did you say " + word + "?");
                if (word.Equals(UnlockWord)) {
                    player.OutputMessage("You said " + word + "; congratulations!");
                    player.CurrentRoom.RoomDelegate = null;
                    player.OutputMessage("\n" + player.CurrentRoom.Description());
                    NotificationCenter.Instance.RemoveObserver("PlayerDidSayWord", PlayerDidSayWord);
                    } else {
                    player.OutputMessage("**You dumb idiot\nyou will not live to see the light of day\n" + word + " is not the word");
                }
                
                }
                
            }
        }

        public string GetNPC(string npcName)
        {
            throw new NotImplementedException();
        }

        NPC IRoomDelegate.GetNPC(string npcName)
        {
            throw new NotImplementedException();
        }

        public string GetNPCS()
        {
            throw new NotImplementedException();
        }
    }

    public class EchoRoom : IRoomDelegate {

        public Room ContainingRoom {
            set; get;
        }

        public EchoRoom() {
         NotificationCenter.Instance.AddObserver("PlayerDidSayWord", PlayerDidSayWord);
        }

        public Door GetExit(string exitName) {
            ContainingRoom.RoomDelegate = null;
            Door exit = ContainingRoom.GetExit(exitName);
            ContainingRoom.RoomDelegate = this;
            return exit;
        }
        public string GetExits() {
            string exits = "";
            if (ContainingRoom.RoomDelegate != null) {
                ContainingRoom.RoomDelegate = null;
                exits += ContainingRoom.GetExits();
                ContainingRoom.RoomDelegate = this;
            }
            return exits;
        }
        public string Description() {
            string description = "You are in an Echo Room.\n";
            ContainingRoom.RoomDelegate = null;
            description += ContainingRoom.Description();
            ContainingRoom.RoomDelegate = this;
            return description;
        }

        public void PlayerDidSayWord(Notification notification) {
            Player player = (Player)notification.Object;
            if (player != null) {
                if (player.CurrentRoom.RoomDelegate == this) {
                    Dictionary<string, Object> userInfo = notification.UserInfo;
                    string word = (string)userInfo["word"];
                    player.OutputMessage("\n" + word + "... " + word + "... " + word + "... \n");

                }

            }
        }

        public string GetNPC(string npcName)
        {
            throw new NotImplementedException();
        }

        NPC IRoomDelegate.GetNPC(string npcName)
        {
            throw new NotImplementedException();
        }

        public string GetNPCS()
        {
            throw new NotImplementedException();
        }
    }

    public class Room
    {
        private Dictionary<string, Door> _exits;
        private string _tag;
        //private IItem _item;
        private IItemContainer _items;
        private NPC _npc;
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        private IRoomDelegate _roomDelegate;
        public IRoomDelegate RoomDelegate {

            set {
                _roomDelegate = value;
                if (_roomDelegate != null) {
                    _roomDelegate.ContainingRoom = this;
                }
            }

            get {
                return _roomDelegate;
            }
        }

        public Room() : this("No Tag"){}

        // Designated Constructor
        public Room(string tag)
        {
            _roomDelegate = null;
            _exits = new Dictionary<string, Door>();
            this.Tag = tag;
            _items = new ItemContainer("Items");
            
        }

        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
        }

        public Door GetExit(string exitName)
        {
            Door door = null;
            if (_roomDelegate != null) {
                door = _roomDelegate.GetExit(exitName);
            } else {
                _exits.TryGetValue(exitName, out door);
            }
            return door;
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            if (_roomDelegate != null) {
                exitNames += _roomDelegate.GetExits();
            } else {
                Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
                foreach (string exitName in keys) {
                    exitNames += " " + exitName;
                }
            }

            return exitNames;
        }

        public NPC GetNPC()
        {
            NPC npc = this._npc;
            if (npc != null)
            {
                return npc;
            }
            return null;
        }
        public string GetNPCS() 
        {
            string npcString = "";
            NPC npc = GetNPC();
            if (npc != null) 
            {
                npcString = npcString + "NPCS: " + npc.Name;
            }
            return npcString;
        }
        public void Drop(NPC NPC)
        {
            _npc = NPC;
        }
        public void Drop(IItem item) 
        {
            _items.Insert(item);
        }


        public IItem PickUp(string itemName)
        {
            IItem itemToReturn = _items.Remove(itemName);

            return itemToReturn;
        }


        public string Description()
        {

            return _roomDelegate!=null?_roomDelegate.Description():"You are " + this.Tag + ".\n *** " + this.GetExits()+"\n >>> Item: " + _items.LongName + this.GetNPCS();
        }
   
    }
}
