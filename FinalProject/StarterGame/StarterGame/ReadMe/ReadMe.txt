Adeola Ogundipe
CPSC 3175 Final Project
The Adventure Game
Game Tittle: HellWorld
Description:
1.	What is the game about, and does it do?
HellWorld is an adventurous game for one player to fight his/her way out of. The starting point is in the dungeon where the player is locked up. 

You are locked up in the dungeon. You finally escaped from your cell, 
and you need to eat to replenish your strength and get some abilities 
in other to fight your way out of this hellish world.
There are different items in all the rooms available. 
You must find these items and weapons which will be helpful to your quest. 
You can quit the game at any point in time.

2.	What are the important implementation features?
There are several keywords which serves as commands that help in the navigation in the game. They are:
•	Help: Tells all the available commands to the player
•	Open: to open a door to a room
•	Go: to move around the rooms
•	Back: to return to previous step
Use: to activate items or weapons
Take: to pick up items or weapon
Attack: to fight
Heal: to replenish and increase strength and battery live.
Change: to change / switch weapons 
•	Pickup
•	Drop
•	Say
•	Inventory
•	Inspect
•	Quit

3.	Which design patterns were used and where they were used?
Game Loop: is used in decouple the progression of game time from user input and processor speed. it processes user input without blocking, updates the game state, and renders the game. It was used in 
Game.cs 
Subclass Sandbox: Define behavior in a subclass using a set of operations provided by its base class.
Item.cs
GameInterface.cs
GameWorld.cs
Player.cs
Room.cs
Singleton
State
Update Method
Flyweight
Command pattern: We define a base class that represents a triggerable game command, then we create subclasses for each of the different game actions. Then input handler, we store a pointer to a command for each button. Then the input handling just delegates to those:
 It was used in the following classes:
•	Command.cs
•	CommandWord.cs
•	Parser.cs
•	GoCommand.cs
•	QuitCommand.cs
•	OpenCommand.cs
•	DropCommand.cs
•	InspectCommand.cs
•	HelpCommand.cs
•	PickUpCommand.cs
•	InventoryCommand.cs
•	SayCommand.cs
Observe lets one piece of code announce that something happened without actually caring who receives the notification.
NotificationCenter.cs
GameWorld.cs
Room.cs

