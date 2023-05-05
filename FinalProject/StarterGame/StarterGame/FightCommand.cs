using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class FightCommand : Command
    {
        public FightCommand() : base()
        {
            this.Name = "fight";
        }

        override
        public bool Execute(Player player)
        {
            player.Fight();
            return false;
        }
    }
}
