using System.Collections;
using System.Collections.Generic;
using System;

namespace HellWorld
{
    public class Player
    {
        private Room _currentRoom = null;
        Random rand = new Random();
        private Stack<Room> _visitedRooms;
        private string _name;
        private Dictionary<string, IItem> _equippedItems;
        private float _MaxWeight;       
        private int _coins;
        private int _health;
        private int _damage;
        private int _armorValue;
        private int _potion;
        private int _weaponValue;
        public enum PlayinPost { PLAYING, WON, LOST }
        private PlayinPost state;
        public PlayinPost State { get { return state; } set { state = value; } }

        private IItemContainer _inventory;
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int Coins { get => _coins; set => _coins = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public int ArmorValue { get => _armorValue; set => _armorValue = value; }
        public int Potion { get => _potion; set => _potion = value; }
        public int WeaponValue { get => _weaponValue; set => _weaponValue = value; }
        public float MaxWeight { get => _MaxWeight; set => _MaxWeight = value; }

        public static void Combat(bool random, string name, int power, int health)
        {

        }

        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }
        
        
        

        public Player(Room room)
        {
            _currentRoom = room;
            _inventory = new ItemContainer("Inventory");
            _visitedRooms = new Stack<Room>();
            _equippedItems = new Dictionary<string, IItem>();
            MaxWeight = 7;
            _equippedItems = new Dictionary<string, IItem>();
            Health = 100;
            Coins = 10;
            Potion = 5;
            ArmorValue = 10;
            WeaponValue = 8;
            Damage = 1;
        }

        public void WaltTo(string direction)
        {
            Door nextDoor = this.CurrentRoom.GetExit(direction);
            if (nextDoor != null)
            {
                if (nextDoor.IsOpen) {
                Room nextRoom = nextDoor.RoomOnTheOtherSideOf(CurrentRoom);
                NotificationCenter.Instance.PostNotification(new Notification("PlayerWillEnterRoom", this));
                _visitedRooms.Push(CurrentRoom);
                this.CurrentRoom = nextRoom;
                NotificationCenter.Instance.PostNotification(new Notification("PlayerHasEnteredRoom", this));
                NormalMessage("\n--- " + this.CurrentRoom.Description());
                } 
                else {
                    WarningMessage("\n****The door on " + direction + " is closed.");
                }
                
            }
            else
            {
                ErrorMessage("\n.....There is no door on " + direction);
            }
        }
        public void GoBack()
        {
            if (_visitedRooms.Count > 0)
            {
                _visitedRooms.TryPop(out _currentRoom);
                NormalMessage("\n **You went back. " + CurrentRoom.Description());
                //OutputMessage("\n" + CurrentRoom.Description());
            }
            else
            {
                WarningMessage("\n **You can't go back any further");
            }
        }
        
        
        public void Say(string word) 
        {
            NormalMessage("Player said " + word);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerDidSayWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }

        public void Open(string doorName) 
        {
            Door door = CurrentRoom.GetExit(doorName);
            if (door != null) 
            {
                if (door.IsOpen) 
                {
                    NormalMessage("\nThe door on " + doorName + " is already open.");
                } 
                else 
                {
                    if (door.Open()) {
                        NormalMessage("\nThe door on " + doorName + " is now open.");
                    } 
                    else {
                        WarningMessage("\nThe door on " + doorName + " cannot be opened.");
                    }                
                }                    
            } 
            else 
            {
                ErrorMessage("\nThere is no door on " + doorName);
            }

        }

        public void Inspect(string itemName) {
            IItem item = CurrentRoom.PickUp(itemName);
            if (item != null) {
                NormalMessage("\n**** " + item.Description);
                CurrentRoom.Drop(item);
            } 
            else {
                ErrorMessage("\nThere is no item named " + itemName + ".");
            }
            
        }
        public void Give(IItem item)
        {
            _inventory.Insert(item);
        }
        public IItem Take(string itemName)
        {
            return _inventory.Remove(itemName);
        }
        public void Inventory()
        {
            NormalMessage("Current Inventory: \n" + _inventory.LongName + "Total weight: " + _inventory.Weight);
        }
        public void Change(string itemName)
        {
            IItem item = CurrentRoom.PickUp(itemName);
            float CurrentWeight = _inventory.Weight;
            if (item != null)
            {
                float carry = CurrentWeight + item.Weight;
                if (MaxWeight < carry)
                {
                    Give(item);
                    NormalMessage("You changed " + itemName + " from the room. ");
                }
                else
                {
                    WarningMessage("You can't change " + itemName + " item is heavy.");
                }
            }
            else
            {
                ErrorMessage("There is no item named " + itemName + " here.");
            }
        }

        public void PickUp(string itemName)
        {
            IItem item = CurrentRoom.PickUp(itemName);
            float CurrentWeight = _inventory.Weight;
            if (item != null)
            {
                float carry = CurrentWeight + item.Weight;
                if (MaxWeight < carry)
                {
                    Give(item);
                    NormalMessage("You picked " + itemName + " from the room. ");
                }
                else
                {
                    WarningMessage("You can't pick " + itemName + " item is heavy.");
                }
            }
            else
            {
                ErrorMessage("There is no item named " + itemName + " here.");
            }
        }
        public void Drop(string itemName)
        {
            IItem item = Take(itemName);
            if(item != null)
            {
                CurrentRoom.Drop(item);
                NormalMessage("You dropped " + itemName + " in the room. ");
            }
            else
            {
                WarningMessage("There is no item named " + itemName + " in the inventory.");
            }
        }
        public void Buy(string itemName)
        {
            IItem item = CurrentRoom.PickUp(itemName);
            if (item != null)
            {
                Give(item);
                NormalMessage("You bought " + itemName + " from the room. ");
            }
            else
            {
                WarningMessage("There is no item named " + itemName + " here.");
            }
        }
        public void Sell(string itemName)
        {
            IItem item = Take(itemName);
            if (item != null)
            {
                CurrentRoom.Drop(item);
                WarningMessage("You Sold " + itemName + " in the room. ");
            }
            else
            {
                ErrorMessage("There is no item named " + itemName + " to be sold.");
            }
        }
        public void Shop()
        {
            Command[] commandArray = { new BuyCommand(), new SellCommand(), new RunCommand() };
            CommandWords shopCommands = new CommandWords(commandArray);
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["commands"] = shopCommands;
            Notification notification = new Notification("Trading Mode", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }
        //fight command changes to battle mode
        public void Fight()
        {
            Command[] commandArray = { new AttackCommand(), new DeffendCommand(), new RunCommand() };
            CommandWords fightCommands = new CommandWords(commandArray);
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["commands"] = fightCommands;
            Notification notification = new Notification("EnterBattle", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }
        //run command
        public void Flee()
        {
            //this.CurrentRoom = GameWorld.Shop;
            NotificationCenter.Instance.PostNotification(new Notification("ExitMode", this));
        }
        public void Attack(string itemValue)
        {
            int damage = 0;
            InfoMessage(" Coins " + Coins + " WeaponValue " + WeaponValue + " Health: " + Health);
            if (damage < 0)
                damage = 0;
            int attack = rand.Next(0, WeaponValue) + rand.Next(1, 4);
            WarningMessage("You lose " + Damage + " health and deal " + attack + "damage");
            Health -= Damage;
            Health -= attack;
        }

        
        public void Deffend()
        {
            int attack = rand.Next(0, WeaponValue) + rand.Next(1, 4);
            WarningMessage("Great deffences " + Damage + " health and deal " + attack + "damage");
        }
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }


    }

}
