using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class RunCommand : Command
    {
        public RunCommand() : base()
        {
            this.Name = "run";
        }

        override
        public bool Execute(Player player)
        {
            player.Flee();
            return false;
        }
    }
}
