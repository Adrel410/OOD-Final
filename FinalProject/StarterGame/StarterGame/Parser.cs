using System.Collections;
using System.Collections.Generic;
using System;

namespace HellWorld
{
    public class Parser
    {
        private Stack<CommandWords> _commands;

        public Parser() : this(new CommandWords()){}

        // Designated Constructor
        public Parser(CommandWords newCommands)
        {
            _commands = new Stack<CommandWords>();
            _commands.Push(newCommands);
            NotificationCenter.Instance.AddObserver("TradingMode", TradingMode);
            NotificationCenter.Instance.AddObserver("ChangeMode", ChangeMode);
            NotificationCenter.Instance.AddObserver("ExitMode", ExitMode);
        }

        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                command = _commands.Peek().Get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.SecondWord = words[1];

                        if(words.Length > 2)
                        {
                            command.ThirdWord = words[2];
                        }
                        else
                        {
                            command.ThirdWord = words[1];
                        }
                    }
                    else
                    {
                        command.SecondWord = null;
                    }
                }
                else
                {
                    // This is debug line of code, should remove for regular execution
                    Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                // This is a debug line of code
                Console.WriteLine("No words parsed!");
            }
            return command;
        }
        public void TradingMode(Notification notification)
        {
            CommandWords newCommands = (CommandWords)notification.UserInfo["commands"];
            _commands.Push(newCommands);
        }
        public void ChangeMode(Notification notification)
        {
            CommandWords newCommands = (CommandWords)notification.UserInfo["commands"];
            _commands.Push(newCommands);
        }
        public void ExitMode(Notification notification)
        {
            _commands.Pop();
        }

        public string Description()
        {
            return _commands.Peek().Description();
        }
    }
}
