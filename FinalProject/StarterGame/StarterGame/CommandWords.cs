﻿using System.Collections;
using System.Collections.Generic;
using System;

namespace HellWorld
{
    public class CommandWords
    {
        private Dictionary<string, Command> commands;
        private static Command[] commandArray = { new GoCommand(), new QuitCommand(), new SayCommand(), new OpenCommand(), new InspectCommand(), new InventoryCommand(), new PickUpCommand(), new DropCommand(), new BackCommand(), new AttackCommand(), new RunCommand(), new DeffendCommand(), new BuyCommand(), new SellCommand(), new FightCommand(), new ShopCommand(), new ChangeCommand() };

        public CommandWords() : this(commandArray) {}

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
