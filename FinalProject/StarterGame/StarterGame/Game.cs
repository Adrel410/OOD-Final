using System.Collections;
using System.Collections.Generic;
using System;

namespace HellWorld
{
    public class Game
    {
        private Player _player;
        private Parser _parser;
        private GameClock _timerInGame;
        private bool _playing;
        public static Player currentPlayer = new Player(GameWorld.Instance.Entrance);

        public Game()
        {
            //GameWorld gw = new GameWorld();
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance.Entrance);
        }

        

        /**
         * GAME LOOP
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n" + _player.Name);
                Command command = _parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    _player.ErrorMessage("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }
        //player's name
        public void Name()
        {
            Console.Write("Enter players name:");
            currentPlayer.Name = Console.ReadLine();
            if (currentPlayer.Name == "")
                Console.WriteLine("You can't remember your name... ");
            else
                Console.WriteLine("You know your name is " + currentPlayer.Name);
        }
        public void Start()
        {
            _timerInGame = new GameClock(10);
            _playing = true;
            _player.InfoMessage(Welcome());
            
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        //public void Win()
        //{
        //    string position = this.CurrentRoom.Tag;
        //    if (position == "heaven")
        //    {
        //        //_playing = false;
        //        OutputMessage(" You Win ");
        //        //Game.End();
        //    }
        //}
        //public void Lose()
        //{
        //    string position = this.CurrentRoom.Tag;
        //    if (position == "Skeleton")
        //    {
        //        //_playing = false;
        //        OutputMessage("You Lose ");
        //    }
        //}

        public string Welcome()
        {
            Console.WriteLine("Welcome to HellWorld where only the strong lives and the week dies!\n\n You are traped in this world of an incredibly boring adventure game.\n\nType 'help' if you need help. ");
            Name();
            return  (_player.CurrentRoom.Description());
        }

        public string Goodbye()
        {
            return "\nThank you for playing HellWorld \nGoodbye!\n";
        }

    }
}
