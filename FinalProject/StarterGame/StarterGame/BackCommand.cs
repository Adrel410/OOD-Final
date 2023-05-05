using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public bool Execute(Player player)
        {
            player.GoBack();
            return false;
        }
    }
}
