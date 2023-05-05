using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class DeffendCommand : Command
    {
        public DeffendCommand() : base()
        {
            this.Name = "deffend";
        }

        override
        public bool Execute(Player player)
        {
            player.Deffend();
            return false;
        }
    }
}
