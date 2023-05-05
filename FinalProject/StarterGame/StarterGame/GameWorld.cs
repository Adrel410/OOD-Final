using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld {
    public class GameWorld {
        private static GameWorld _instance = null;
        private Room _shop;

        public Room Shop { get => _shop; set => _shop = value; }
        public static GameWorld Instance {
            get {
                if (_instance == null) {
                    _instance = new GameWorld();
                }
                return _instance;
            }
        }

        private Room _entrance;
        public Room Entrance { get { return _entrance; } }
        private Room _exit;
        public Room Exit { get { return _exit; } }
        private int counter;
        private Room _win;
        public Room Win { get { return _win; } }
        private Room _lose;
        public Room Lose { get { return _lose; } }



        private Dictionary<Room,IWorldEvent> worldEvents;
        //private Dictionary<Room, List<IWorldEvent>> worldEvents;
        //private WorldMod worldMod;

        private GameWorld() {
            worldEvents = new Dictionary<Room, IWorldEvent>();
            CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
            NotificationCenter.Instance.AddObserver("PlayerWillEnterRoom", PlayerWillEnterRoom);
            counter = 0;
        }

        public void PlayerDidEnterRoom(Notification notification) 
        {
            Player player = (Player)notification.Object;
            if (player != null) {
                if (player.CurrentRoom == Exit) 
                {
                    player.WarningMessage("\n*** The player reached the exit ");
                    player.State = Player.PlayinPost.WON;
                    counter++;
                    if (counter == 5) {
                        //Exit.SetExit("shortcut", Entrance);
                        //Entrance.SetExit("shortcut", Exit);
                    }

                }
                worldEvents.TryGetValue(player.CurrentRoom, out IWorldEvent worldEvent);

                if (worldEvent != null) {
                    worldEvent.Execute();
                    player.OutputMessage("There is a change in the world.");
                    RemoveWorldEvent(worldEvent);

                }
            }
        }


        public void PlayerWillEnterRoom(Notification notification) {
            Player player = (Player)notification.Object;
            if (player != null) {
                if (player.CurrentRoom == Entrance) {
                    player.WarningMessage("\n>>> the player is leaving the entrance");
                }
                if (player.CurrentRoom == Exit) {
                    player.WarningMessage("\n >>> The player is going away from the exit. ");
                }
            }
        }

        public void AddWorldEvent(IWorldEvent worldEvent) {
            worldEvents[worldEvent.Trigger] = worldEvent;
        }

        private void RemoveWorldEvent(IWorldEvent worldEvent) {
            worldEvents.Remove(worldEvent.Trigger);
        }

        private void CreateWorld()
        {
            //Creating Rooms to walk through

            Room dungeon = new Room("in the dungeon, the entrance of the game where the journey begins.");
            Room hallOfFame = new Room("in the hall of fame. Is there anythig you need? I can't help you, so to the store");
            Room buringFlame = new Room("in the buringFlame. ha! haa!!");
            Room celler = new Room("in the cellar");
            Room forestPark = new Room("in the forest. You think you can escape?");
            Room store = new Room("in the store. Are you ready to purchase something?");
            Room devilsDomain = new Room("in the Devil's Domain. \n Welcome my child!!!, don't run from me.");
            Room skeleton = new Room("in the skelton hall. This is where your live will end and you will join the other bones");
            Room heaven = new Room("in heaven. Welcome to your safe place, you can rest a little before you continue.");
            Room zombie = new Room("in the zombie Center");
            Room humanCultist = new Room("i the cultist room. Do you wish to join us? We are the cult people");
            Room grave = new Room("in the grave yard.");
            Room humanRorue = new Room("in a room and you have just encountered a humanRorue.Can you kill thwe Human Rorue ");
            

            //Creating doors

            Door door = Door.CreateDoor(buringFlame, dungeon, "east", "west");
            door = Door.CreateDoor(store, buringFlame, "west", "south");
            door = Door.CreateDoor(devilsDomain, buringFlame, "east", "west");
            door = Door.CreateDoor(celler, buringFlame, "south", "north");
            door = Door.CreateDoor(store, hallOfFame, "east", "west");
            door = Door.CreateDoor(heaven, store, "south", "north");
            door.Close();
            ILockable regularLock = new RegularLock();
            door.TheLock = regularLock;
            regularLock.Lock();

            door = Door.CreateDoor(heaven, skeleton, "north", "south");
            door = Door.CreateDoor(heaven, devilsDomain, "east", "west");
            door = Door.CreateDoor(celler, skeleton, "east", "west");
            door = Door.CreateDoor(forestPark, celler, "south", "north");
            door = Door.CreateDoor(humanCultist, zombie, "east", "west");
            door = Door.CreateDoor(grave, humanCultist, "south", "north");
            door = Door.CreateDoor(humanRorue, humanCultist, "north", "south");
            door = Door.CreateDoor(store, heaven, "north", "east");

            // Setup Connection

            IWorldEvent worldMod = new WorldMod(forestPark, heaven, zombie, "west", "east");
            AddWorldEvent(worldMod);

            worldMod = new WorldMod(store, forestPark, grave, "north", "south");
            AddWorldEvent(worldMod);

            worldMod = new WorldMod(humanRorue, skeleton, store, "west", "east");
            AddWorldEvent(worldMod);

            //trap room time :] (delegates)
            IRoomDelegate trapRoom = new TrapRoom();
            devilsDomain.RoomDelegate = trapRoom;
            //trapRoom.ContainingRoom = devilsDomain;
            //trapRoom.ContainingRoom = celler;

            IRoomDelegate echoRoom = new EchoRoom();
            grave.RoomDelegate = echoRoom;
            //echoRoom.ContainingRoom = forestPark;

            forestPark.RoomDelegate = echoRoom;
            echoRoom.ContainingRoom = grave;


            //Create/drop itmes in rooms
            IItem item = new Item("dagger", 1.5f, 2f);
            IItem decorator = new Item("ruby", 0.5f, 10f, true);
            item.AddDecorator(decorator);
            decorator = new Item("gold", 0.75f, 15f, true);
            item.AddDecorator(decorator);
            buringFlame.Drop(item);

            item = new Item("coins", 2f, 3);
            humanCultist.Drop(item);
            decorator = new Item("gold", 0.75f, 15f, true);
            item.AddDecorator(decorator);

            IItemContainer chest = new ItemContainer("chest", 1, 2);
            heaven.Drop(chest);
            item = new Item("Money", 0.1f, 50f);
            chest.Insert(item);

            item = new Item("coins", 2f, 3);
            heaven.Drop(item);

            item = new Item("clothes", 2f, 1f);
            forestPark.Drop(item);

            item = new Item("Fire", 0.5f, 1f);
            buringFlame.Drop(item);

            item = new Item("sword", 5f, 5f);
            dungeon.Drop(item);
            item = new Item("buckler", 10f, 7f, true);
            dungeon.Drop(item);

            //dropping monster in assigned room
            NPC enemy = new NPC("HumanRouge", 1.5f, 2f);
            humanRorue.Drop(enemy);

            enemy = new NPC("Zombie", 5f, 5f);
            zombie.Drop(enemy);

            enemy = new NPC("Skeleton", 1f, 3f);
            skeleton.Drop(enemy);

            //items in the shop to purchase
            IItemContainer snipper = new ItemContainer("snipper", 1, 2);
            heaven.Drop(snipper);
            item = new Item("bullets", 10, 50f);
            snipper.Insert(item);

            item = new Item("clothes", 0.1f, 50f);
            chest.Insert(item);
            item = new Item("gun", 0.1f, 150f);
            store.Drop(item);
            item = new Item("bullets", 5f, 150f);
            store.Drop(item);
            item = new Item("dragonSword", 0.6f, 3f);
            store.Drop(item);
            item = new Item("flameSword", 1f, 2f);
            store.Drop(item);
            item = new Item("diamod", 0.1f, 150);
            store.Drop(item);
            item = new Item("coins", 7f, 100);
            store.Drop(item);
            item = new Item("gold", 0.1f, 90);
            store.Drop(item);
            item = new Item("sliver", 0.1f, 70);
            store.Drop(item);

            // Assign special rooms
            _entrance = dungeon;
            _exit = celler;
            _win = heaven;
            _lose = skeleton;
            Shop = store;
            // return dungeon;
        }

    }
}
